<template>
    <div>
        <div v-if="errorMessage">{{errorMessage}}</div>
        <div v-else>
            <router-link
                v-if="correctUser()"
                class="nav-link text-dark"
                to="/brands/create"
            >{{commonTranslations.filter(t => t.code == "Create New")[0].name}}</router-link>
            <table class="table">
                <thead>
                    <tr>
                        <th>{{brandsViewTranslations.filter(t => t.code == "Name")[0].name}}</th>
                        <th>{{brandsViewTranslations.filter(t => t.code == "Description")[0].name}}</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="brand in brands" :key="brand.id">
                        <td>{{brand.name}}</td>
                        <td>{{brand.description}}</td>
                        <td>
                            <router-link
                                class="text-dark"
                                :to="`brands/details/${brand.id}`"
                            >{{commonTranslations.filter(t => t.code == "Details")[0].name}}</router-link>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { IBrands } from "../../domain/Brands/IBrands";
import store from "@/store";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class BrandsIndex extends Vue {
    errorMessage = "";

    get brands(): IBrands[] {
        return store.state.brands;
    }

    get userRole(): string {
        return store.getters.userRole;
    }

    correctUser(): boolean {
        const role = store.getters.userRole;
        return role.includes("Admin") || role.includes("Publisher");
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
        const response = store.dispatch("getBrands");
        response
            .then((response) => {
                console.log(response.statusCode);
                if (response.statusCode >= 400 && response.statusCode < 500) {
                    this.errorMessage = "NOT ALLOWED";
                } else {
                    this.errorMessage = JSON.parse(
                        response.errorMessage
                    ).message;
                }
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
