import Moment from "moment";

export default {
  data() {
    return {
      term: null,
      tags: [],
      filterTags: [],
      posts: []
    };
  },
  async mounted() {
    await this.loadPosts();
    await this.loadTags();
  },
  methods: {
    async loadPosts() {
      var { data: posts } = await this.axios.get('posts');
      posts.forEach(p => p.createdTime = Moment(p.createdTime).format("DD/MM/YYYY HH:mm"));
      this.posts = posts;
    },
    async loadTags() {
      var { data: tags } = await this.axios.get('tags');
      this.tags = tags;
    },
    async applyFilter() {
      await this.loadPosts();

      if (this.term) {
        this.posts = this.posts.filter(p => p.content.toLowerCase().includes(this.term.toLowerCase()) || p.title.toLowerCase().includes(this.term.toLowerCase()));
      }

      if (this.filterTags.length) {
        const filteredIds = this.filterTags.map(ft => ft.id);
        this.posts = this.posts.filter(p => p.tags.map(t => t.id).some(id => filteredIds.includes(id)));
      }

      if (!this.term && !this.filterTags.length) {
        await this.resetFilter();
      }

    },
    async resetFilter() {
      this.term = null;
      this.filterTags = [];
      await this.loadPosts();
    }
  },
};
