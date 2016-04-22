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


        public fridgeItems;
        public predicate;
        public reverse;

        constructor(private $http: ng.IHttpService) {
            $http.get('/api/Items')
                .then((response) => {
                    this.fridgeItems = response.data;
                })
                .catch((response) => {
                    console.log(response.data);
                })
        }

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
        public categories;
        public selectedCategory;
        public selectedCategories = [];
        public foodCategories = ["Dairy", "Frozen", "Refrigerated", "Meat", "Vegetable", "Fruit", "Other"];
        constructor(private $http: ng.IHttpService) {
            // get categories
        }

        postItem() {
            this.categories = this.selectedCategories;
            this.$http.post("/api/items", {
                name: this.name,
                expDate: this.expDate,
                categories: this.selectedCategories //may need to add value: 0 in case post fails.
            })
                .then(response => {
                    console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                })
        }

        newCategory(category, index) {
            console.log(index);
            this.selectedCategories.push({ name: category });
            console.log(this.selectedCategories);
          
        }
    }

}
