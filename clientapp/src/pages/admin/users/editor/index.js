import { VueEditor } from "vue2-editor";

export default {
  components: {
    VueEditor
  },
  data() {
    return {
      user: {
        firstName: null,
        lastName: null,
        email: null,
        password: null,
      }
    };
  },
  async mounted() {
    if (this.$route.params.id) {
      var { data: user } = await this.axios.get(`users/${this.$route.params.id}`);
      this.user = user;
    }
  },
  computed: {
    buttonName() {
      return this.$route.params.id ? "Оновити" : "Створити";
    },
  },
  methods: {
    createOrUpdate() {
      if (this.$route.params.id) {
        this.update();
      }
      else {
        this.create();
      }
    },
    create() {
      this.axios
        .post('users', this.user)
        .then(() => this.$router.push({ name: "admin-users" }));
    },
    update() {
      this.axios
        .put(`users/${this.$route.params.id}`, this.user)
        .then(() => this.$router.push({ name: "admin-users" }));
    }
  },
};
