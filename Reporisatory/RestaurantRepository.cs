using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public class RestaurantRepository : IGenericReporisatory<Restaurant>, IRestaurantRepository
    {
        private readonly Reservecotexet context;

        public RestaurantRepository(Reservecotexet context)
        {
            this.context = context;
        }
        public void Delete(int id)
        {
            Restaurant restaurant = GetById(id);
            if (restaurant != null)
            {
                context.restaurants.Remove(restaurant);
            }
        }

        public List<Restaurant> GetAll()
        {
            return context.restaurants.ToList();
        }

        public Restaurant GetById(int id)
        {
            return context.restaurants.FirstOrDefault(r => r.RestaurantId == id);
        }

        public void Insert(Restaurant obj)
        {
            context.restaurants.Add(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Restaurant obj)
        {
            context.Update(obj);
        }
    }
}
