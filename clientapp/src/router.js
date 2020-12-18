import Vue from "vue";
import Router from "vue-router";

Vue.use(Router);
const routes = [
  {
    path: "/",
    redirect: "/posts",
    name: "root",
  },
  {
    path: "/posts",
    component: () => import("./pages/posts/index.vue"),
    name: "posts",
  },
  {
    path: "/posts/:id",
    component: () => import("./pages/posts/details/index.vue"),
    name: "post-details",
  },
  {
    path: "/admin",
    name: "admin",
    redirect: "/admin/posts",
    component: () => import("./pages/admin/index.vue"),
    children: [
      {
        path: "/admin/posts",
        name: "admin-posts",
        component: () => import("./pages/admin/posts/index.vue"),
      },
      {
        path: "/admin/posts/editor/:id",
        name: 'editor',
        component: () => import("./pages/admin/posts/editor/index.vue"),
      },
      {
        path: "/admin/tags",
        name: "admin-tags",
        component: () => import("./pages/admin/tags/index.vue"),
      },
      {
        path: "/admin/tags/editor/:id",
        name: 'tag-editor',
        component: () => import("./pages/admin/tags/editor/index.vue"),
      },
      {
        path: "/admin/users",
        name: "admin-users",
        component: () => import("./pages/admin/users/index.vue"),
      },
      {
        path: "/admin/users/editor/:id",
        name: 'user-editor',
        component: () => import("./pages/admin/users/editor/index.vue"),
      },
      {
        path: "/admin/users/password-changer/:id",
        name: 'user-password-changer',
        component: () => import("./pages/admin/users/password-changer/index.vue"),
      },
      {
        path: "/admin/sign-in",
        name: "sign-in",
        component: () => import("./pages/admin/sign-in/index.vue"),
      },
      {
        path: "/admin/sources",
        name: "admin-sources",
        component: () => import("./pages/admin/sources/index.vue"),
      },
      {
        path: "/admin/sources/editor/:id",
        name: 'source-editor',
        component: () => import("./pages/admin/sources/editor/index.vue"),
      },
    ],
  },
  {
    path: "*",
    redirect: "/404",
  },
];




const router = new Router({
  mode: "history",
  routes,
});


export default router;
