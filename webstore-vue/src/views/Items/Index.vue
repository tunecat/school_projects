<template>
    <div>
        <div v-if="statusErrorMessage">{{statusErrorMessage}}</div>
        <div v-else>
            <router-link
                class="nav-link text-dark"
                to="/items/create"
            >{{commonTranslations.filter(t => t.code == "Create New")[0].name}}</router-link>

            <table class="table">
                <thead>
                    <tr>
                        <th>{{itemsViewTranslations.filter(t => t.code == "Name")[0].name}}</th>
                        <th>{{itemsViewTranslations.filter(t => t.code == "Description")[0].name}}</th>
                        <th>{{itemsViewTranslations.filter(t => t.code == "Price")[0].name}}</th>
                        <th>{{itemsViewTranslations.filter(t => t.code == "Discount")[0].name}}</th>
                        <th>{{itemsViewTranslations.filter(t => t.code == "Brand")[0].name}}</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in items" :key="item.id">
                        <td>{{item.name}}</td>
                        <td>{{item.description}}</td>
                        <td
                            style="text-decoration: line-through"
                            v-if="item.discount !== 0"
                        >{{item.price}}</td>
                        <td v-else>{{item.price}}</td>
                        <td
                            v-if="item.discount !== 0"
                        >{{item.discount}}% -> {{item.itemPriceWithDiscount}}</td>
                        <td v-else>None</td>

                        <td>{{item.brand.name}}</td>
                        <td>
                            <router-link
                                class="text-dark"
                                :to="`items/details/${item.id}`"
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
import store from "@/store";
import IItem from "@/domain/Item/IItem";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class ItemsIndex extends Vue {
    statusErrorMessage = "";

    get items(): IItem[] {
        return store.state.items;
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
        const response = store.dispatch("getItems");
        response
            .then((response) => {
                console.log(response.statusCode);
                if (response.statusCode >= 400 && response.statusCode < 500) {
                    this.statusErrorMessage = "NOT ALLOWED";
                } else {
                    this.statusErrorMessage = JSON.parse(
                        response.statusErrorMessage
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
        console.log(commonTranslation);
        return commonTranslation;
    }
}
</script>
