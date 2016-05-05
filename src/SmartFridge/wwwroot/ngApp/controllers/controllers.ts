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
        public categoriesEdit;
        public newCategories: any = [];
        public foodCategories = ["Dairy", "Frozen", "Fruit", "Grain", "Junk", "Leftovers", "Protein", "Refrigerated", "Vegetable", "Other"];
        public categoryImages = [
            {
                name: 'Dairy',
                img: '../../images/cheese.png',
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
                img: '../../images/fridgeimage.png',
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
        
        // #justGoWithIt #theresProbablyAFilterForThis #YOLO #heldTogetherWithDuctTape #RedNeckCode #DanTheMan #DanWasHere #NeedMoreCoffee
        public editItemCallback(origItem) {
            console.log('............editItemCallback...........');
            console.log(origItem);
            console.log(`origItem name: ${origItem.name}`);
            console.log(`origItem expDate: ${origItem.expDate}`);
            console.log(`origItem categories: ${origItem.categories}`);
            let name;
            let expDate;
            let categories;
            if (this.newName = !null) {
                name = this.newName;
            }
            else {
                name = origItem.name;
            }
            console.log(`editItem name: ${name}`);
            if (this.newExpDate = !null) {
                expDate = this.newExpDate;
            }
            else {
                expDate = origItem.expDate;
            }
            console.log(`editItem expDate: ${expDate}`);
            if (this.newCategories = !null) {
                categories = this.newCategories;
            }
            else {
                categories = origItem.categories;
            }
            console.log(`editItem categories: ${categories}`);
            this.editItem({
               
                    name,
                    expDate,
                    categories
               
            }, origItem);
        }
        public editItem(itemToEdit, origItem) {
            console.log('............editItem............');
            console.log(`editItem: ${
                itemToEdit.name,
                itemToEdit.expDate,
                itemToEdit.catergories                    
                }, origItem: ${
                origItem.name,
                origItem.expDate,
                origItem.categories
                }`);
            let newItem = {
                itemToEdit,
                origItem
            }
            this.$http.put(`/api/Items/Edit`, newItem)











        //public editItem(itemToEdit) {

        //    this.newCategories.forEach((category) => {
        //        this.categoriesEdit.push({ name: category })
        //    });
        //    console.log(itemToEdit); //debug
        //    if (this.newName == null || this.newName == undefined || this.newName == "") {
        //        this.newName = itemToEdit.name;
        //    }
        //    if (this.newExpDate == null || this.newExpDate == undefined) {
        //        this.newExpDate = itemToEdit.expDate;
        //    }
        //    let items = {
        //        currItem: {
        //            //addedDate: itemToEdit.addedDate,
        //            expDate: itemToEdit.expDate,
        //            name: itemToEdit.name,
        //            categories: itemToEdit.categories
        //        },
        //        newItem: {
        //            name: this.newName,
        //            expDate: this.newExpDate,
        //            categories: this.categoriesEdit
        //        }
        //    };
        //    //this.$http.put(`/api/Items/Edit`, itemToEdit)
        //    this.$http.put(`/api/Items/Edit`, items)
        //        .then((response) => {
        //            console.log(response);
        //            //refresh current state
        //            this.$state.go(this.$state.current, {}, { reload: true });
        //        }).catch((response) => {
        //            console.log(response);
        //        });
        //}

        public deleteItem(itemToGo) {
            //console.log(itemToGo); //DEBUG

            this.$http.post(`/api/Items/Delete`, itemToGo)
                .then((response) => {
                    console.log(response);
                    //refresh current state
                    this.$state.go(this.$state.current, {}, { reload: true });
                }).catch((response) => {
                    console.log(response);
                });

        }
       
        public testPut(origItem) {
            console.log(origItem);
            console.log(this.newName);
            console.log(this.newExpDate);
            console.log(this.newCategories);
            
        }

        public toggleItem(category) {
            let idx = this.newCategories.indexOf(category);
            if (idx >= 0) {
                this.newCategories.splice(idx, 1);
            }
            else {
                this.newCategories.push(category);
            }
        }

        public showForm() {
            if (this.editView == false) {
                this.editView = true;
            }
            else {
                this.editView = false;
                this.newCategories = [];
                this.newName = null;
                this.newExpDate = null;
            }
        }

        public getImage(item): any {
            if (item.categories.length >= 1) {
                return this.categoryImages.filter((category, x) => {
                    return this.categoryImages[x].name === item.categories[0].name;
                })[0].img;
            }
            else {
                return this.categoryImages[7].img;
            }
        }

        public setDaysLeft(expDate) {
            let numDaysLeft = this.$filter('amDifference')(expDate, null, 'days');
            return numDaysLeft;
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
