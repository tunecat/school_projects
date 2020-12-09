<template>
    <div class="row">
        <div class="col-xs-12 col-md-3">
            <div style="height: 90px">
                <h2 style="text-align: center">{{sharedTranslations.filter(t => t.code == "Profile")[0].name}}</h2>
            </div>
            <hr />
            <ul class="list-group">
                <li class="list-group-item d-flex justify-content-between align-items-center">
                    <a class="nav-link" @click="chooseEntity('Products')">{{sharedTranslations.filter(t => t.code == "Products")[0].name}}</a>
                    <span class="badge badge-primary badge-pill">
                        <i style="font-size: 130%" class="fa fa-archive"></i>
                    </span>
                </li>
                <li class="list-group-item d-flex justify-content-between align-items-center">
                    <a class="nav-link" @click="chooseEntity('Brands')">{{sharedTranslations.filter(t => t.code == "Brands")[0].name}}</a>
                    <span class="badge badge-primary badge-pill">
                        <i style="font-size: 130%" class="fa fa-copyright"></i>
                    </span>
                </li>
                <li class="list-group-item d-flex justify-content-between align-items-center">
                    <a class="nav-link text-dark" @click="chooseEntity('Categories')">{{sharedTranslations.filter(t => t.code == "Categories")[0].name}}</a>
                    <span class="badge badge-primary badge-pill">
                        <i style="font-size: 130%" class="fa fa-align-justify"></i>
                    </span>
                </li>
                <li class="list-group-item d-flex justify-content-between align-items-center">
                    <a class="nav-link text-dark" @click="chooseEntity('Warehouses')">{{sharedTranslations.filter(t => t.code == "Warehouses")[0].name}}</a>
                    <span class="badge badge-primary badge-pill">
                        <i style="font-size: 130%" class="fa fa-home"></i>
                    </span>
                </li>
            </ul>
        </div>
        <div class="col-xs-12 col-md-9">
            <div style="height: 90px">
                <h1 style="text-align: center">{{commonTranslations.filter(t => t.code == "ManageYourEntities")[0].name}}</h1>
                <h4 style="text-align: center" v-if="entity != ''">{{sharedTranslations.filter(t => t.code == entity)[0].name}}</h4>
            </div>
            <hr />
            <div id="entityComponent">
                <itemsIndex v-if="entity == 'Products'" />
                <warehousesIndex v-if="entity == 'Warehouses'" />
                <categoriesIndex v-else-if="entity == 'Categories'" />
                <brandsIndex v-else-if="entity == 'Brands'" />
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import router from "@/router";
import store from "@/store";
import ItemsIndex from "../Items/Index.vue";
import WarehousesIndex from "../Warehouses/Index.vue";
import CategoriesIndex from "../Categories/Index.vue";
import BrandsIndex from "../Brands/Index.vue";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component({
    components: {
        ItemsIndex,
        BrandsIndex,
        WarehousesIndex,
        CategoriesIndex,
    },
})
export default class Profile extends Vue {
    get entity() {
        return store.state.profileEntity;
    }

    chooseEntity(entity: string) {
        store.dispatch("setProfileState", entity);
    }

    // ============ Lifecycle methods ==========
    beforeCreate() {
        console.log("beforeCreate");
    }

    created() {
        console.log("created");
    }

    beforeMount() {
        console.log("beforeMount");
    }

    mounted() {
        console.log("mounted");
    }

    beforeUpdate() {
        console.log("beforeUpdate");
    }

    updated() {
        console.log("updated");
    }

    beforeDestroy() {
        console.log("beforeDestroy");
    }

    destroyed() {
        console.log("destroyed");
    }

    get itemsViewTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "item") return t;
        })[0].cultureDTO;
        return ItemsViewTranslation;
    }

    get sharedTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const sharedTranslation = translations!.filter((t) => {
            if (t.entityName === "shared") return t;
        })[0].cultureDTO;
        return sharedTranslation;
    }

    get commonTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const commonTranslation = translations!.filter((t) => {
            if (t.entityName === "common") return t;
        })[0].cultureDTO;
        return commonTranslation;
    }
}
</script>

<style>
</style>
