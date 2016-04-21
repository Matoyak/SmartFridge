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
        public newItem;
        public selectedCategory;
        public selectedCategories = [];
        public foodCategories = ["Dairy", "Frozen", "Refrigerated", "Meat", "Vegetable", "Fruit", "Other"];
        constructor(private $http: ng.IHttpService) { }

        testItem() {
            this.newItem.categories = this.selectedCategories;
            console.log(this.newItem);
        }
        postItem() {

            this.newItem.categories = this.selectedCategories;
            console.log(this.newItem);
            this.$http.post("/api/items", this.newItem)
                .then((response) => {
                    console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                })
        }

        newCategory() {

            this.selectedCategories.push(this.selectedCategory);
            console.log(this.selectedCategories);
        }
    }

}
