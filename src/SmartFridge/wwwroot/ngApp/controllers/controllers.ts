namespace SmartFridge.Controllers {

    export class HomeController {
        public message = 'Hello from the home page!';
    }


    export class SecretController {
        public secrets;

        constructor($http: ng.IHttpService) {
            $http.get('/api/secrets').then((results) => {
                this.secrets = results.data;
            });
        }
    }

    export class ViewFridgeController {

        public selectedItem;
        public fridgeItems;
        public predicate;
        public reverse;
        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService, private $filter) {

            $http.get('/api/Items')
                .then((response) => {
                    this.fridgeItems = response.data;
                })
                .catch((response) => {
                    console.log(response.data);
                })
        }

        public deleteItem(itemToGo) {
            console.log(itemToGo);
            
            this.$http.delete(`/api/delete`, itemToGo)
                .then((response) => {
                    console.log(response);
                    this.$state.go('myFridge');
                })
                .catch((response) => {
                    console.log(response);
                })
        }

        getColor(daysLeft) {
            daysLeft = this.$filter('amDifference')(daysLeft, null, 'days');

            if (daysLeft <= 0) {
                return 'red';
            }

            switch (Math.floor(daysLeft / 3) + 1) {
                case 1:
                    return 'orange';
                case 2:
                    return 'yellow';
            }

            return 'green';
        }

        // Orderby method to orderby any of the property...........
        public order(property) {
            if (property === this.predicate) {
                this.reverse = !this.reverse;
            }
            else {
                this.predicate = property;
                this.reverse = false;
            }
        }

    }

    export class AddItemController {
        public name;
        public expDate;
        public categories = [];
        public selectedCategory;
        public selectedCategories: any = [];
        public foodCategories = ["Dairy", "Frozen", "Refrigerated", "Protein", "Vegetable", "Fruit", "Other"];

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService) {
            // get categories
        }
        postItem() {
            this.selectedCategories.forEach((category) => {
                this.categories.push({ name: category })
            });
            this.$http.post("/api/items", {
                name: this.name,
                expDate: this.expDate,
                categories: this.categories //may need to add value: 0 in case post fails.
            })
                .then((response) => {
                    this.$state.go("myFridge");
                })
                .catch((response) => {
                    console.log(response.data);
                })
        }

        toggleItem(category) {
            let idx = this.selectedCategories.indexOf(category);
            if (idx >= 0) {
                this.selectedCategories.splice(idx, 1);
            }
            else {
                this.selectedCategories.push(category);
            }
        }
    }
}
