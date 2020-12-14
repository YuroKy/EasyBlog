import Vue from "vue";
import Router from "vue-router";

Vue.use(Router);
const routes = [
  {
    path: "/",
    component: () => import("./pages/posts/index.vue"),
    name: "posts",
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
