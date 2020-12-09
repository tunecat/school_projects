import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import VeeValidate from "vee-validate";
import VueSimpleAlert from "vue-simple-alert";

import "jquery";
import "popper.js";
import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import 'font-awesome/css/font-awesome.min.css';
import i18n from './i18n'

Vue.config.productionTip = false

Vue.use(VueSimpleAlert);
Vue.use(VeeValidate, { events: "blur" });

VeeValidate.Validator.extend("verify_password", {
    getMessage: field =>
        `The password must contain at least: 1 uppercase letter, 1 lowercase letter, 1 number, and one special character (E.g. , . _ & ? etc)`,
    validate: value => {
        const strongRegex = new RegExp(
            "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,})"
        );
        return strongRegex.test(value);
    }
});

new Vue({
    router,
    store,
    i18n,
    render: h => h(App)
}).$mount("#app");
