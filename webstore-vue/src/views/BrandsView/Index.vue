<template>
    <div class="row">
        <div v-for="brand in brandsView" :key="brand.id" class="col-sm-6 col-md-4 col-lg-3">
            <router-link :to="`/brandsView/details/${brand.id}`">
                <div class="card">
                    <img
                        class="card-img-top"
                        style="height: 240px;"
                        src="https://lezebre.lu/images/detailed/13/nike-logo.png"
                        alt
                    />
                    <div class="card-body">
                        <p class="card-text">{{brand.name}}</p>
                    </div>
                </div>
            </router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import router from "@/router";
import store from "@/store";
import { ICart } from "../../domain/Cart/ICart";
import { IBrands } from "../../domain/Brands/IBrands";

@Component
export default class BrandsViewIndex extends Vue {
    errorMessage = "";

    get brandsView(): IBrands[] | null {
        const brands = store.state.brands;
        return brands;
    }

    isAuthenticated(): boolean {
        return store.getters.isAuthenticated;
    }

    // ============ Lifecycle methods ==========
    beforeCreate(): void {
        console.log("beforeCreate");
    }

    created(): void {
        console.log("created");
    }

    beforeMount(): void {
        console.log("beforeMount");
    }

    mounted(): void {
        store
            .dispatch("getBrandsView")
            .then(response => {
                this.errorMessage = JSON.parse(response.errorMessage).message;
            })
            .catch(e => {
                console.log(e);
            });
        console.log("mounted");
    }

    beforeUpdate(): void {
        console.log("beforeUpdate");
    }

    updated(): void {
        console.log("updated");
    }

    beforeDestroy(): void {
        console.log("beforeDestroy");
    }

    destroyed(): void {
        console.log("destroyed");
    }
}
</script>
