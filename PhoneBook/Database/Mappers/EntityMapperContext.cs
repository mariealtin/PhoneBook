namespace PhoneBook.Database.Mappers
{
    public class EntityMapperContext
    {
        private IEntityMapper mapper;

        public EntityMapperContext(IEntityMapper mapper)
        {
            this.mapper = mapper;
        }

        public void Map()
        {
            mapper.MapEntity();
        }
    }
}