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
        public fridgeItems; //entire collection of items in database
        public predicate; //filter component
        public reverse; //filter component
        public editView = false;
        public newName;
        public newExpDate;
        public newCategeries: any = [];
        public foodCategories = ["Dairy", "Frozen", "Fruit", "Grain", "Junk", "Leftovers", "Protein", "Refrigerated", "Vegetable", "Other"];
        public categoryImages = [
            {
                name: 'Dairy',
                img: '../../images/dairy.png', //this is an egg? Eggs aren't dairy.
            },
            {
                name: 'Frozen',
                img: '../../images/frozen.png',
            },
            {
                name: 'Fruit',
                img: '../../images/fruit2.png',
            },
            {
                name: 'Protein',
                img: '../../images/protien.png',
            },
            {
                name: 'Junk',
                img: '../../images/other2.png',
            },
            {
                name: 'Vegetable',
                img: '../../images/vegetables.png',
            },
            {
                name: 'Grain',
                img: '../../images/other1.png',
            },
            {
                name: 'None',
                img: '../../images/girllogo.png',
            },
            {
                name: 'Other',
                img: '../../images/fruit1.png',
            },
            {
                name: 'Leftovers',
                img: '../../images/other2.png',
            },
            {
                name: 'Refrigerated',
                img: '../../images/FridgeImage.png',
            }

        ];

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService, private $filter) {
            //get items from database
            $http.get('/api/Items')
                .then((response) => {
                    this.fridgeItems = response.data;
                }).catch((response) => {
                    console.log(response.data);
                });
        }

        public editItem(itemToEdit) {
            this.$http.put(`/api/Items/Delete`, itemToEdit)
                .then((response) => {
                    console.log(response);
                    //refresh current state
                    this.$state.go(this.$state.current, {}, { reload: true });
                }).catch((response) => {
                    console.log(response);
                });
        }

        public deleteItem(itemToGo) {
            //console.log(itemToGo); //DEBUG
            let deleteItem = confirm("Are you sure you want to delete this item?");
            if (deleteItem) {
                this.$http.post(`/api/Items/Delete`, itemToGo)
                    .then((response) => {
                        console.log(response);
                        //refresh current state
                        this.$state.go(this.$state.current, {}, { reload: true });
                    }).catch((response) => {
                        console.log(response);
                    });
            }
        }

        public testPut() {
            console.log("walalalalalaaa");
        }

        public toggleItem(category) {
            let idx = this.newCategeries.indexOf(category);
            if (idx >= 0) {
                this.newCategeries.splice(idx, 1);
            }
            else {
                this.newCategeries.push(category);
            }
        }

        public showForm() {
            if (this.editView == false) {
                this.editView = true;
            }
            else {
                this.editView = false;
                this.newCategeries = [];
                this.newName = null;
                this.newExpDate = null;
            }
        }

        public getImage(item) {
            let img;
            if (item.categories.length >= 1) {
                this.categoryImages.forEach((category, x) => {
                    if (this.categoryImages[x].name === item.categories[0].name) {
                        img = this.categoryImages[x].img;
                        return;
                    }
                })
                return img;
            }
            else {
                return this.categoryImages[7].img;
            }
        }

        public getColor(daysLeft) {
            daysLeft = this.$filter('amDifference')(daysLeft, null, 'days');
            if (daysLeft <= 0) {
                return 'red';
            } switch (Math.floor(daysLeft / 3) + 1) {
                case 1:
                    return 'orange';
                case 2:
                    return 'yellow';
            }
            return 'green';
        }

        // Orderby method to order by any of the property...
        public order(property) {
            if (property === this.predicate) {
                this.reverse = !this.reverse;
            } else {
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
        public foodCategories = ["Dairy", "Frozen", "Fruit", "Grain", "Junk", "Leftovers", "Protein", "Refrigerated", "Vegetable", "Other"];

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService) { }

        public postItem() {
            this.selectedCategories.forEach((category) => {
                this.categories.push({ name: category })
            });
            this.$http.post("/api/Items", {
                name: this.name,
                expDate: this.expDate,
                categories: this.categories //may need to add value: 0 in case post fails.
            }).then((response) => {
                this.$state.go("myFridge");
            }).catch((response) => {
                console.log(response.data);
            });
        }

        public toggleItem(category) {
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
