<template>
    <div>
        <div>
            <h1>{{commonTranslations.filter(t => t.code == "Details")[0].name}}</h1>
            <div>
                <h4>{{itemsViewTranslations.filter(t => t.code == "Brand")[0].name}}</h4>
                <hr />
                <dl class="row">
                    <dt class="col-sm-2">{{brandViewTranslations.filter(t => t.code == "Name")[0].name}}</dt>
                    <dd class="col-sm-10">{{brandDetailsView.name}}</dd>
                    <dt class="col-sm-2">{{brandViewTranslations.filter(t => t.code == "Description")[0].name}}</dt>
                    <dd class="col-sm-10">{{brandDetailsView.description}}</dd>
                </dl>
            </div>
            <div>
                <a @click="SeeAllBrandItems()">{{brandViewTranslations.filter(t => t.code == "BrandProducts")[0].name}}</a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import router from "@/router";
import store from "@/store";
import { ICart } from "../../domain/Cart/ICart";
import { IBrands } from "../../domain/Brands/IBrands";
import IFilterSearch from "../../domain/FilterSearch/IFilterSearch";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class BrandsViewDetails extends Vue {
    @Prop()
    private id!: string;

    errorMessage = "";

    get brandDetailsView(): IBrands | null {
        const brand = store.state.brand;
        return brand;
    }

    SeeAllBrandItems(): void {
        const searchDTO: IFilterSearch = {
            Search: "",
            brandsFilter: [this.brandDetailsView!.id],
            categoriesFilter: null,
        };
        store
            .dispatch("postItemsView", searchDTO)
            .then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    router.push("/ItemsView/true");
                }
            })
            .catch((e) => {
                console.log(e);
            });
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
            .dispatch("getBrandView", this.id)
            .then((response) => {
                this.errorMessage = JSON.parse(response.errorMessage).message;
            })
            .catch((e) => {
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

    get itemsViewTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "item") return t;
        })[0].cultureDTO;
        return ItemsViewTranslation;
    }

    get commonTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const commonTranslation = translations!.filter((t) => {
            if (t.entityName === "common") return t;
        })[0].cultureDTO;
        return commonTranslation;
    }

    get brandViewTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const sharedTranslation = translations!.filter((t) => {
            if (t.entityName === "brand") return t;
        })[0].cultureDTO;
        return sharedTranslation;
    }
}
</script>
