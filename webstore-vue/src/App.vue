<template>
    <div>
        <header>
            <nav
                class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark border-bottom box-shadow mb-3"
            >
                <div class="container">
                    <router-link class="navbar-brand" to="/itemsView">Web Store</router-link>
                    <button
                        class="navbar-toggler"
                        type="button"
                        data-toggle="collapse"
                        data-target=".navbar-collapse"
                        aria-controls="navbarSupportedContent"
                        aria-expanded="false"
                        aria-label="Toggle navigation"
                    >
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                        <identity />
                        <ul class="navbar-nav flex-grow-1">
                            <li class="nav-item">
                                <router-link
                                    class="nav-link text-dark"
                                    to="/itemsView"
                                >{{sharedTranslations.filter(t => t.code == "Products")[0].name}}</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link class="nav-link text-dark" to="/brandsView">{{sharedTranslations.filter(t => t.code == "Brands")[0].name}}</router-link>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
        <div class="container">
            <main
                role="main"
                class="pb-3"
                style="padding: 10px; box-shadow: 0px 21px 267px -116px rgba(0,0,0,0.75);"
            >
                <router-view></router-view>
            </main>
        </div>
    </div>
</template>

<style src="./assets/css/main.css"></style>
<script src="/node_modules/vue-cookie/build/vue-cookie.js'"></script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import Identity from "./components/Identity.vue";

// Tell Vue to use the plugin

import Multiselect from "vue-multiselect";
import store from "./store";
import CultureDTO from "./domain/Translations/CultureDTO";
import i18n from "./i18n";
Vue.component("multiselect", Multiselect);

@Component({
    components: {
        Identity,
    },
})
export default class App extends Vue {
    get sharedTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const sharedTranslation = translations!.filter((t) => {
            if (t.entityName === "shared") return t;
        })[0].cultureDTO;
        return sharedTranslation;
    }

    mounted() {
        store.dispatch("setCultures");
        store.dispatch("setTranslations");
        console.log("mounted APP");
    }
}
</script>
