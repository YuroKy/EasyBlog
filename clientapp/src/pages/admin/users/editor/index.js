import { VueEditor } from "vue2-editor";

export default {
  components: {
    VueEditor
  },
  data() {
    return {
      user: {
        avatar: null,
        uploadedAvatar: null,
        username: null,
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
    avatarPreview() {
      return `data:image/png;base64, ${this.user.avatar}`;
    }
  },
  methods: {
    async createOrUpdate() {
      const user = Object.assign({}, this.user);
      let formData = new FormData();
      formData.append('avatar', user.uploadedAvatar)
      formData.append('username', user.username)
      formData.append('firstName', user.firstName)
      formData.append('lastName', user.lastName)
      formData.append('email', user.email)
      formData.append('password', user.password)

      if (this.$route.params.id) {
        this.update(formData);
        this.$toast.success("Користувач успішно оновлений!");
      }
      else {
        this.create(formData);
        this.$toast.success("Користувач успішно створений!");
      }
    },
    create(user) {
      this.axios
        .post('users', user)
        .then(() => this.$router.push({ name: "admin-users" }));
    },
    update(user) {
      this.axios
        .put(`users/${this.$route.params.id}`, user)
        .then(() => this.$router.push({ name: "admin-users" }));
    }
  },
};
