import Moment from "moment";

export default {
  data() {
    return {
      post: {},
    };
  },
  async mounted() {
    var { data: post } = await this.axios.get(`posts/${this.$route.params.id}`);
    post.createdTime = Moment(post.createdTime).format("DD/MM/YYYY HH:mm");
    this.post = post;
  },
  methods: {
    getAuthorAvatar(post) {
      return `data:image/png;base64, ${post.authorAvatar}`;
    },
  },
};
