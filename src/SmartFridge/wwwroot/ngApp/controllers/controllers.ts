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

        constructor(private $http: ng.IHttpService) {
            $http.get(`/api/Items`)
                .then((response) => {
                    this.fridgeItems = response.data;
                })
                .catch((response) => {
                    console.log(response.data);
                })
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
