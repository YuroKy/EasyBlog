import Moment from "moment";

export default {
  data() {
    return {
      posts: []
    };
  },
  async mounted() {
    var { data: posts } = await this.axios.get('posts');

    if (!posts.length) {
      this.$router.push({ name: "editor" });
    }

    posts.forEach(p => p.createdTime = Moment(p.createdTime).format("DD/MM/YYYY HH:mm"));

    this.posts = posts;
  },
  methods: {
  },
};
