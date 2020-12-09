import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Home from "../views/Home.vue";
import BrandsIndex from "../views/Brands/Index.vue";
import BrandsDetails from "../views/Brands/Details.vue";
import BrandsCreate from "../views/Brands/Create.vue";
import BrandsEdit from "../views/Brands/Edit.vue";
import Login from "../views/Account/Login.vue";
import Register from "../views/Account/Register.vue";
import Profile from "../views/Account/Profile.vue";
import CategoriesIndex from "../views/Categories/Index.vue";
import CategoriesCreate from "../views/Categories/Create.vue";
import CategoriesCreateChild from "../views/Categories/CreateChild.vue";
import CategoriesEdit from "../views/Categories/Edit.vue";
import ItemsInWarehouseDetails from "../views/ItemsInWarehouse/Details.vue";
import WarehousesIndex from "../views/Warehouses/Index.vue";
import WarehouseDetails from "../views/Warehouses/Details.vue";
import WarehouseEdit from "../views/Warehouses/Edit.vue";
import WarehouseCreate from "../views/Warehouses/Create.vue";
import ItemsIndex from "../views/Items/Index.vue";
import ItemsCreate from "../views/Items/Create.vue";
import ItemsEdit from "../views/Items/Edit.vue";
import ItemsDetails from "../views/Items/Details.vue";
import ItemsViewIndex from "../views/ItemsView/Index.vue";
import ItemsViewDetails from "../views/ItemsView/Details.vue";
import UserWishListIndex from "../views/UserWishList/Index.vue";
import CartViewIndex from "../views/CartView/Index.vue";
import BrandsViewIndex from "../views/BrandsView/Index.vue";
import BrandsViewDetails from "../views/BrandsView/Details.vue";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
    { path: "/account/login", name: "Login", component: Login },
    { path: "/account/register", name: "Register", component: Register },

    { path: "/", name: "Home", component: Home },

    // Customer
    { path: "/itemsView/:post?", name: "ItemsView", component: ItemsViewIndex, props: true },
    {
        path: "/itemsView/details/:id?",
        name: "ItemsViewDetails",
        component: ItemsViewDetails,
        props: true
    },

    { path: "/userWishList", name: "UserWishList", component: UserWishListIndex },
    { path: "/cartView", name: "CartView", component: CartViewIndex },

    { path: "/brandsView", name: "BrandsView", component: BrandsViewIndex },
    {
        path: "/brandsView/details/:id?",
        name: "BrandsViewDetails",
        component: BrandsViewDetails,
        props: true
    },

    // Business Client

    { path: "/profile", name: "Profile", component: Profile },

    { path: "/brands", name: "Brands", component: BrandsIndex },
    {
        path: "/brands/details/:id?",
        name: "BrandsDetails",
        component: BrandsDetails,
        props: true
    },
    { path: "/brands/create", name: "BrandsCreate", component: BrandsCreate },
    {
        path: "/brands/edit/:id?",
        name: "BrandsEdit",
        component: BrandsEdit,
        props: true
    },

    { path: "/categories", name: "Categories", component: CategoriesIndex },
    {
        path: "/categories/edit/:id?",
        name: "CategoriesEdit",
        component: CategoriesEdit,
        props: true
    },
    {
        path: "/categories/create/:id?",
        name: "CategoriesCreate",
        component: CategoriesCreate,
        props: true
    },
    {
        path: "/categories/createChild/:id?",
        name: "CategoriesCreateChild",
        component: CategoriesCreateChild,
        props: true
    },

    {
        path: "/itemsInWarehouse/details/:id?",
        name: "ItemsInWarehouseDetails",
        component: ItemsInWarehouseDetails,
        props: true
    },

    { path: "/warehouses", name: "Warehouses", component: WarehousesIndex },
    { path: "/warehouses/details/:id?", name: "WarehouseDetails", component: WarehouseDetails, props: true },
    { path: "/warehouses/create", name: "WarehouseCreate", component: WarehouseCreate },
    {
        path: "/warehouses/edit/:id?",
        name: "WarehousesEdit",
        component: WarehouseEdit,
        props: true
    },

    { path: "/items", name: "Items", component: ItemsIndex },
    {
        path: "/items/details/:id?",
        name: "ItemsDetails",
        component: ItemsDetails,
        props: true
    },
    { path: "/items/create", name: "ItemsCreate", component: ItemsCreate },
    {
        path: "/items/edit/:id?",
        name: "ItemsEdit",
        component: ItemsEdit,
        props: true
    }
];

const router = new VueRouter({
    routes
});

export default router;
