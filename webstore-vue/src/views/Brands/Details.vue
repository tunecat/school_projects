<template>
    <div>
        <div v-if="errorMessage">{{errorMessage}}</div>
        <div v-else>
        <h1>{{commonTranslations.filter(t => t.code == "Details")[0].name}}</h1>
        <div>
            <h4>{{itemsViewTranslations.filter(t => t.code == "Brand")[0].name}}</h4>
            <hr />
            <dl v-if="brand" class="row">
                <dt class="col-sm-2">{{brandsViewTranslations.filter(t => t.code == "Name")[0].name}}</dt>
                <dd class="col-sm-10">{{brand.name}}</dd>
                <dt class="col-sm-2">{{brandsViewTranslations.filter(t => t.code == "Description")[0].name}}</dt>
                <dd class="col-sm-10">{{brand.description}}</dd>
            </dl>
        </div>
        <div>
            <router-link class="btn btn-primary" :to="`../edit/${id}`">{{commonTranslations.filter(t => t.code == "Edit")[0].name}}</router-link>|
            <a class="btn btn-danger" @click="onDelete()">{{commonTranslations.filter(t => t.code == "Delete")[0].name}}</a>
            <router-link class="btn" :to="`/Profile`">{{commonTranslations.filter(t => t.code == "BackToList")[0].name}}</router-link>
        </div>
    </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IBrands } from "../../domain/Brands/IBrands";
import store from "@/store";
import router from "@/router";
import CultureDTO from '@/domain/Translations/CultureDTO';

@Component
export default class BrandsDetails extends Vue {
    @Prop()
    private id!: string;

    errorMessage = "";

    get brand(): IBrands | null {
        return store.state.brand;
    }

    get userRole(): string {
        return store.getters.userRole;
    }

    onDelete(): void {
        this.$confirm("Are you sure you want to delete this")
            .then(() => {
                store
                    .dispatch("deleteBrand", this.id)
                    .then((wasDeleted: boolean) => {
                        if (wasDeleted) {
                            router.push("/Brands");
                        }
                    });
            })
            .catch(e => console.log(e));
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
            .dispatch("getBrand", this.id)
            .then(response => {
                console.log(response.statusCode);
                if (response.statusCode >= 400 && response.statusCode < 500) {
                    this.errorMessage = "NOT ALLOWED";
                } else {
                    this.errorMessage = JSON.parse(
                        response.errorMessage
                    ).message;
                }
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

    get brandsViewTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "brand") return t;
        })[0].cultureDTO;
        return ItemsViewTranslation;
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
