<template>
    <div>
        <h1>Register</h1>

        <div class="row">
            <div class="col-md-4">
                <h4>Create a new account.</h4>
                <h4 style="color: red" v-if="registerResponse">{{registerResponse}}</h4>
                <hr />
                <div v-for="error in errors" :key="error.id" class="text-danger">
                    <div>{{ error.msg }}</div>
                </div>
                <div class="form-group">
                    <label for="email">Email</label>
                    <input
                        v-validate="'required|email'"
                        v-model="registerInfo.email"
                        class="form-control"
                        type="email"
                        id="email"
                        name="email"
                    />
                </div>
                <div class="form-group">
                    <label for="First Name">First Name</label>
                    <input
                        v-validate="'required|min:3|max:128'"
                        v-model="registerInfo.firstName"
                        class="form-control"
                        type="text"
                        id="First_Name"
                        maxlength="128"
                        name="First Name"
                    />
                </div>
                <div class="form-group">
                    <label for="Last Name">Last Name</label>
                    <input
                        v-validate="'required|min:3|max:128'"
                        v-model="registerInfo.lastName"
                        class="form-control"
                        type="text"
                        id="Last Name"
                        maxlength="128"
                        name="Last Name"
                    />
                </div>
                <div class="form-group">
                    <label for="password">Password</label>
                    <input
                        v-model="registerInfo.password"
                        v-validate="'required|min:3|verify_password'"
                        class="form-control"
                        type="password"
                        id="password"
                        ref="password"
                        maxlength="100"
                        name="password"
                    />
                </div>
                <div class="form-group">
                    <label for="password_confirmation">Confirm password</label>
                    <input
                        v-validate="'required|confirmed:password'"
                        data-vv-as="password"
                        class="form-control"
                        type="password"
                        id="password_confirmation"
                        name="password_confirmation"
                    />
                </div>
               <button
                    v-if="errors.all().length === 0"
                    type="submit"
                    @click="onRegister()"
                    class="btn btn-primary"
                >Register</button>
                <button v-else type="submit" class="btn btn-primary">Register</button>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IRegisterDTO } from "../../types/IRegisterDTO";
import router from "@/router";
import store from "@/store";

@Component
export default class Register extends Vue {
    private registerResponse: string | null = null;

    private registerInfo: IRegisterDTO = {
        firstName: "",
        lastName: "",
        email: "",
        password: ""
    };

    validateInfo(): boolean {
        return (
            this.registerInfo.firstName.length > 0 &&
            this.registerInfo.lastName.length > 0 &&
            this.registerInfo.password.length > 0 &&
            this.registerInfo.email.length > 0
        );
    }

    onRegister(): void {
        if (!this.validateInfo()) {
            this.registerResponse = "Input fields";
        } else {
            store.dispatch("registerUser", this.registerInfo).then(response => {
                if (response.statusCode === 200) {
                    this.registerResponse = null;
                    router.push("/");
                } else {
                    this.registerResponse = response.errorMessage;
                }
            });
        }
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
