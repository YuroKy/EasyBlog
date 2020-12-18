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
import Vuex from 'vuex';

Vue.use(Vuex);
Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
Vue.use(VueToast, {
  position: 'top-right'
})

Vue.component('multiselect', Multiselect);

const store = new Vuex.Store({
  state: {
    token: null,
  },
  mutations: {
    updateToken(state, token) {
      state.token = token;
    }
  },
  getters: {
    token: state => {
      return state.token;
    }
  }
});


Axios.defaults.baseURL = window.location.origin + "/api/";
Axios.interceptors.request.use(function (config) {
  const token = store.getters.token;
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
}, function (err) {
  return Promise.reject(err);
});
Vue.use(VueAxios, Axios);




new Vue({
  store,
  router: Router,
  render: h => h(App),
  created() {
  },
}).$mount('#app');
