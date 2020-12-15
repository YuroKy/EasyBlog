import { VueEditor } from "vue2-editor";

export default {
  components: {
    VueEditor
  },
  data() {
    return {
      post: {
        title: null,
        content: null,
        tags: [],
      },
      tags: [],
    };
  },
  async mounted() {
    var { data: tags } = await this.axios.get('tags');
    this.tags = tags;

    if (this.$route.params.id) {
      var { data: post } = await this.axios.get(`posts/${this.$route.params.id}`);
      this.post = post;
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
        this.$toast.success("Новина успішно оновлена!");
      }
      else {
        this.create();
        this.$toast.success("Новина успішно створена!");
      }
    },
    create() {
      this.axios
        .post('posts', mapForApi(this.post))
        .then(() => this.$router.push({ name: "admin-posts" }));
    },
    update() {
      this.axios
        .put(`posts/${this.$route.params.id}`, mapForApi(this.post))
        .then(() => this.$router.push({ name: "admin-posts" }));
    }
  },
};

function mapForApi(post) {
  const clone = Object.assign({}, post);
  clone.tagIds = clone.tags.map(pt => pt.id);
  return clone;
}
