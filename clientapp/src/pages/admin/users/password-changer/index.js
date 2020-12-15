import { VueEditor } from "vue2-editor";

export default {
  components: {
    VueEditor
  },
  data() {
    return {
      user: {
        password: null,
      }
    };
  },
  async mounted() {
  },
  methods: {
    changePassword() {
      this.axios
        .put(`users/change-password/${this.$route.params.id}`, this.user)
        .then(() => this.$router.push({ name: "admin-users" }));
    },
  },
};
