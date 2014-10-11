using System;
using Common;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class ContactService : RepositoryBase<Contact>, IContactService
    {
        private readonly ITagService _iTagService;

        public ContactService(IDatabaseFactory databaseFactory, IUserInfo userInfo, ITagService iTagService)
            : base(databaseFactory, userInfo)
        {
            _iTagService = iTagService;
        }

        public override void Save(Guid? id, Contact entity)
        {
            entity.Pinyin = Spell.MakeSpellCode(entity.ContactName, SpellOptions.EnableUnicodeLetter);

            if (!string.IsNullOrEmpty(entity.Tag))
                foreach (string tag in entity.Tag.Split(' '))
                {
                    if (!string.IsNullOrEmpty(tag))
                        _iTagService.Save(null, new Tag {TagName = tag, TagType = "Contect"});
                }

            base.Save(id, entity);
        }
    }
}