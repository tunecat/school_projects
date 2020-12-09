<template>
    <div>
        <div v-if="statusErrorMessage">{{statusErrorMessage}}</div>
        <div v-else>
            <p>
                <router-link
                    :to="`/categories/CreateChild`"
                >{{commonTranslations.filter(t => t.code == "Create New")[0].name}}</router-link>
            </p>
            <p v-if="correctUser()">
                <router-link
                    :to="`/categories/Create`"
                >{{categoriesViewTranslations.filter(t => t.code == "CreateNewParentCategory")[0].name}}</router-link>
            </p>

            <ul v-for="category in categories" :key="category.id" class="list-group">
                <h4>{{category.name}}</h4>
                <li
                    v-for="child in category.childCategories"
                    :key="child.id"
                    class="list-group-item"
                >
                    <a>
                        <h5>{{child.name}}</h5>
                    </a>
                    <div v-if="correctUser()">
                        <router-link style="color: black"
                            :to="`/categories/Edit/${child.id}`"
                        >{{commonTranslations.filter(t => t.code == "Edit")[0].name}} | </router-link>
                        <a @click="onDelete(child.id)"
                        >{{commonTranslations.filter(t => t.code == "Delete")[0].name}}</a>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import ICategory from "../../domain/Category/ICategory";
import router from "@/router";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class CategoriesIndex extends Vue {
    statusErrorMessage = "";

    get categories(): ICategory[] | null {
        return store.state.categories;
    }

    onDelete(id: string): void {
        this.$confirm("Are you sure you want to delete this")
            .then(() => {
                store
                    .dispatch("deleteCategory", id)
                    .then((wasDeleted: boolean) => {
                        if (wasDeleted) {
                            store.dispatch("getCategories");
                        }
                    });
            })
            .catch((e) => console.log(e));
    }

    correctUser(): boolean {
        const role = store.getters.userRole;
        return role.includes("Admin");
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
        const response = store.dispatch("getCategories");
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

    get categoriesViewTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "category") return t;
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
