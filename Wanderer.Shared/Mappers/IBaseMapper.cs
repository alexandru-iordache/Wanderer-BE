namespace Wanderer.Shared.Mappers;

public interface IBaseMapper<TEntity, TDto, TInsertDto>
    where TEntity : class
    where TDto : class
    where TInsertDto : class
{
    TDto MapToDto(TEntity entity);

    TEntity MapToEntity(TDto dto);

    TEntity MapToEntity(TInsertDto insertDto, params object[] parameters);
}