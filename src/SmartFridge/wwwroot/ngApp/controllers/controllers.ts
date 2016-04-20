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
        public testItems = [
            {
                name:"Apple",
                expDate: "April 6",
                categories: "Fruit"
            },
            {
                name:"Milk",
                expDate:"April 28",
                categories: "Dairy"
            },
            {
                name: "Steak",
                expDate: "May 20",
                categories: "Meat"
            },
            {
                name: "Eggs",
                expDate:"May 10",
                categories: "Dairy"
            },
            {
                name: "Cheese",
                expDate:"June 1",
                categories: "Dairy"
            }
        ];

        constructor(private $http: ng.IHttpService) {
            $http.get(`/api/Items`)
                .then((response) => {
                    this.fridgeItems = response.data;
                })
                .catch((response) => {
                    console.log(response.data);
                });
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

        constructor(private $http: ng.IHttpService) { }

        postItem() {
            this.$http.post(`/api/Items`, this.newItem)
                .then((response) => {
                    console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                })
        }
    }

}
