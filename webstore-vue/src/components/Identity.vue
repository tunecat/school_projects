<template>
    <ul class="navbar-nav">
        <li class="nav-item" style="margin-top: 8px">
                <div class="search-container" style="min-width: 222px">
                    <input v-model="searchBar" type="text" placeholder="Search.." />
                    <button @click="Search()" type="submit">
                        <i class="fa fa-search"></i>
                    </button>
                </div>
            </li>
        <template v-if="isAuthenticated">
            <li class="nav-item">
                <router-link class="nav-link text-dark" to="/userWishList">
                    <i class="fa fa-heart"></i>
                </router-link>
            </li>
            <li class="nav-item">
                <router-link class="nav-link text-dark" to="/CartView">
                    <i class="fa fa-shopping-cart"></i>
                </router-link>
            </li>
            <li class="nav-item">
                <router-link to="/Profile" class="nav-link text-dark">
                    <i class="fa fa-user"></i>
                </router-link>
            </li>
            <li class="nav-item">
                <a @click="onLogout" class="nav-link text-dark">{{sharedTranslations.filter(t => t.code == "Logout")[0].name}}</a>
            </li>
        </template>

        <template v-else>
            <li class="nav-item">
                <router-link class="nav-link text-dark" to="/Account/Register">Register</router-link>
            </li>
            <li class="nav-item">
                <router-link class="nav-link text-dark" to="/Account/Login">Login</router-link>
            </li>
        </template>
        <li class="nav-item dropdown">
            <a
                class="nav-link dropdown-toggle"
                href="#"
                id="navbarDropdown"
                role="button"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
            >{{translations.filter(t => t.code == "Language")[0].name}} ({{currentCulture.code}})</a>
            <div
                class="dropdown-menu"
                style="background-color: #343a40 !important"
                aria-labelledby="navbarDropdown"
            >
                <a
                    v-for="culture in cultures"
                    :key="culture.code"
                    @click="setCulture(culture)"
                    class="dropdown-item language-item"
                >{{culture.name}}</a>
            </div>
        </li>
    </ul>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import router from "../router";
import jwtDecode from "jwt-decode";
import IFilterSearch from "@/domain/FilterSearch/IFilterSearch";
import CultureDTO from "@/domain/Translations/CultureDTO";
import UICulture from "../domain/Translations/UICultures";
import { set } from "vue/types/umd";
import i18n from "@/i18n";

@Component
export default class Identity extends Vue {
    get currentCulture(): UICulture {
        return store.state.currentCulture;
    }

    get sharedTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const sharedTranslation = translations!.filter((t) => {
            if (t.entityName === "shared") return t;
        })[0].cultureDTO;
        return sharedTranslation;
    }

    setCulture(culture: UICulture) {
        i18n.locale = culture.code;
        store.dispatch("setCurrentCulture", culture);
        store.dispatch("setTranslations");
    }

    get translations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "shared") return t;
        })[0].cultureDTO;
        return ItemsViewTranslation;
    }

    get cultures(): UICulture[] {
        return store.state.cultures;
    }

    get isAuthenticated(): boolean {
        return store.getters.isAuthenticated;
    }

    searchBar = "";

    Search(): void {
        const searchDTO: IFilterSearch = {
            Search: this.searchBar,
            brandsFilter: null,
            categoriesFilter: null,
        };
        store
            .dispatch("postItemsView", searchDTO)
            .then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.searchBar = "";
                    router.push("/ItemsView/true");
                }
            })
            .catch((e) => {
                console.log(e);
            });
    }

    onLogout(): void {
        store.dispatch("clearJwt");
        router.push("/");
    }
}
</script>
