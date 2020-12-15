import Vue from 'vue';
import App from './App.vue';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import Router from "./router";
import Axios from 'axios'
import VueAxios from 'vue-axios'
import Multiselect from 'vue-multiselect'
import VueToast from 'vue-toast-notification';
// Import one of the available themes
//import 'vue-toast-notification/dist/theme-default.css';
import 'vue-toast-notification/dist/theme-sugar.css';

Axios.defaults.baseURL = window.location.origin;

Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
Vue.use(VueAxios, Axios);
Vue.use(VueToast, {
  position: 'top-right'
})
Vue.component('multiselect', Multiselect);

new Vue({
  router: Router,
  render: h => h(App),
  created() {
  },
}).$mount('#app');
