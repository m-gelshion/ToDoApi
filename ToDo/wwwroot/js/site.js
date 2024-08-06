$(function () {
    function AppViewModel() {
        var self = this;

        this.newListName = ko.observable('');
        this.newItemName = ko.observable('');

        this.users = ko.observableArray([]);
        this.currentUser = ko.observable(null);
        this.currentListIndex = ko.observable(-1);
        this.currentItemIndex = ko.observable(-1);
        this.isListValid = ko.computed(
            function () {
                return self.newListName() && self.newListName().length > 2;
            }
        );
        this.isItemValid = ko.computed(
            function () {
                return self.newItemName() && self.newItemName().length > 2;
            }
        );

        this.lists = ko.observableArray([]);

        ko.computed(
            function () {
                if (self.currentUser()) {
                    self.refreshLists();
                }
            }
        );

        this.refreshLists = function () {
            $.get('/api/users/'+self.currentUser().id+'/ToDoList')
                .then(function (listData) {
                    self.lists(_.map(listData, ToDoList.prototype.create));
                    self.clearSelection();
                });
        };

        this.refreshUsers = function () {
            $.get('/api/user')
                .then(function (userData) {
                    self.users(userData);
                });
        };

        this.saveList = function () {
            var user = self.currentUser();
            $.ajax({
                type: "POST",
                url: '/api/users/' + user.id + '/ToDoList',
                data: JSON.stringify(self.newListName()),
                contentType: 'application/json',
                dataType: 'json',
                success: function (newList) {
                    self.lists.push(new ToDoList(newList));
                    self.clearSelection();
                }
            });
        };

        this.removeList = function () {
            var user = self.currentUser();
            var list = self.lists()[self.currentListIndex()];
            $.ajax({
                type: "DELETE",
                url: '/api/users/' + user.id + '/ToDoList/' + list.id(),
                success: function () {
                    self.lists.remove(list);
                }
            });
        };

        this.saveItem = function () {
            var user = self.currentUser();
            var list = self.lists()[self.currentListIndex()];
            $.ajax({
                type: "POST",
                url: '/api/users/' + user.id + '/ToDoList/'+ list.id() +'/item',
                data: JSON.stringify(self.newItemName()),
                contentType: 'application/json',
                dataType: 'json',
                success: function (newItem) {
                    self.lists()[self.currentListIndex()]
                        .items.push(new ToDoItem(newItem));
                    self.clearSelection();
                }
            });
        };

        this.removeItem = function () {
            var user = self.currentUser();
            var item = self.lists()[self.currentListIndex()].items()[self.currentItemIndex()];
            $.ajax({
                type: "DELETE",
                url: '/api/users/' + user.id + '/ToDoItem/' + item.id(),
                success: function () {
                    self.lists()[self.currentListIndex()].items.remove(item);
                }
            });
        };

        this.editList = function (index) {
            self.currentListIndex(index());
            self.newListName(self.lists()[index()].title());
        };

        this.editItem = function (index) {
            self.currentItemIndex(index());
            var item = self.lists()[self.currentListIndex()]
                .items()[index()];
            self.newItemName(item.title());
        };

        this.updateList = function () {
            var user = self.currentUser();
            var list = self.lists()[self.currentListIndex()];
            var newListName = self.newListName();
            $.ajax({
                type: "PUT",
                url: '/api/users/' + user.id + '/ToDoList/' + list.id(),
                data: JSON.stringify(newListName),
                contentType: 'application/json',
                dataType: 'json',
                success: function () {
                    self.lists()[self.currentListIndex()]
                        .title(newListName);
                    self.clearSelection();
                }
            });
        };

        this.updateItem = function (item) {
            var user = self.currentUser();
            var item = self.lists()[self.currentListIndex()].items()[self.currentItemIndex()];
            var newItemName = self.newItemName();
            $.ajax({
                type: "PUT",
                url: '/api/users/' + user.id + '/ToDoItem/' + item.id(),
                data: JSON.stringify(newItemName),
                contentType: 'application/json',
                dataType: 'json',
                success: function () {
                    self.lists()[self.currentListIndex()]
                        .items()[self.currentItemIndex()]
                        .title(newItemName);
                    self.clearSelection();
                }
            });
        };

        this.clearSelection = function () {
            self.currentItemIndex(-1);
            self.newItemName(null);
            self.newListName(null);
        }

        this.selectList = function (index) {
            self.currentListIndex(index());
        };

        this.selectItem = function (index) {
            self.currentItemIndex(index());
        };
    }

    var vm = new AppViewModel();

    ko.applyBindings(vm);

    vm.refreshUsers();

    function ToDoList(data) {
        var self = this;

        var itm = data.items;
        this.title = ko.observable(data.title || '');
        this.id = ko.observable(data.id || 0);
        this.items = ko.observableArray(
            itm
                ? _.map(itm, ToDoItem.prototype.create)
                : []
        );
        this.isComplete = ko.computed(
            function () {
                return self.items() && self.items().length && _.every(self.items(), function (item) { return item.isComplete(); });
            }
        );
        this.listCompleteCss = ko.computed(
            function () {
                return self.isComplete()
                    ? "alert-success"
                    : "alert-secondary";
            }
        )
    }

    function ToDoItem(data) {
        var self = this;

        this.id = ko.observable(data.id || 0);
        this.title = ko.observable(data.title || '');
        this.isComplete = ko.observable(data.isComplete || false);
        this.itemCompleteCss = ko.computed(
            function () {
                return self.isComplete()
                    ? "list-group-item-success"
                    : "list-group-item-light";
            }
        )

        this.toggleItemCompletion = function (item) {
            var user = vm.currentUser();
            var isComplete = item.isComplete();
            var toggleRoute = isComplete
                ? 'clear'
                : 'complete';
            $.ajax({
                type: "PUT",
                url: '/api/users/' + user.id + '/ToDoItem/' + item.id() + '/' + toggleRoute,
                success: function () {
                    item.isComplete(!isComplete);
                }
            });
        };
    }

    ToDoList.prototype.create = function (data) {
        return new ToDoList(data);
    }

    ToDoItem.prototype.create = function (data) {
        return new ToDoItem(data);
    }
});