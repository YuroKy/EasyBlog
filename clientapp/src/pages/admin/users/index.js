export default {
  data() {
    return {
      users: [],
      fields: [
        {
          key: 'id',
          label: 'Ідентифікатор',
        },
        {
          key: 'username',
          label: 'Логін',
        },
        {
          key: 'firstName',
          label: "Ім'я",
        },
        {
          key: 'lastName',
          label: "Фамілія",
        },
        {
          key: 'email',
          label: "Пошта",
        },
        {
          key: 'status',
          label: "Стан",
          formatter: (status) => status === 0 ? 'Активний' : 'Заблокований'
        },
        {
          key: 'Дії',
          label: 'Дії',
        }
      ],
    };
  },
  async mounted() {
    await this.loadUsers();
  },
  methods: {
    async loadUsers() {
      var { data: users } = await this.axios.get('users');
      this.users = users;
    },
    async remove(user) {
      await this.axios.delete(`users/${user.id}`);
      this.users = this.users.filter(p => p.id != user.id);
      this.$toast.success("Користувач успішно видалений!");
    },
    edit(user) {
      this.$router.push({ name: 'user-editor', params: { id: user.id } });
    },
    create() {
      this.$router.push({ name: 'user-editor' });
    },
    changePassword(user) {
      this.$router.push({ name: 'user-password-changer', params: { id: user.id } });
    },
    async changeUserStatus(user) {
      const action = user.status === 0 ? 'block' : 'unblock';
      const resultMessage = user.status === 0 ? 'заблокований' : 'розблокований';

      await this.axios.put(`users/${action}/${user.id}`);
      await this.loadUsers();
      this.$toast.success(`Користувач успішно ${resultMessage}!`);
    }
  },
};