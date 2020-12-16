import Moment from "moment";

export default {
  data() {
    return {
      posts: [],
      fields: [
        {
          key: 'id',
          label: 'Ідентифікатор',
        },
        {
          key: 'title',
          label: 'Заголовок',
        },
        {
          key: 'authorName',
          label: 'Редактор',
        },
        {
          key: 'createdTime',
          label: 'Дата створення',
        },
        {
          key: "Дії",
          lable: "Дії",
        }
      ],
    };
  },
  async mounted() {
    var { data: posts } = await this.axios.get('posts');
    posts.forEach(p => p.createdTime = Moment(p.createdTime).format("DD/MM/YYYY HH:mm"));

    this.posts = posts;
  },
  methods: {
    async remove(post) {
      await this.axios.delete(`posts/${post.id}`);
      this.posts = this.posts.filter(p => p.id != post.id);
      this.$toast.success("Новина успішно видалена!");
    },
    edit(post) {
      console.log(post);
      this.$router.push({ name: 'editor', params: { id: post.id } });
    },
    create() {
      this.$router.push({ name: 'editor' });
    }
  },
};