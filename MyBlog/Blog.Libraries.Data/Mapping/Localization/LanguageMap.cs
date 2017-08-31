﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Domain.Localization;

namespace Blog.Libraries.Data.Mapping.Localization
{

    public class LanguageMap : CustomEntityTypeConfiguration<Language>
    {

        public LanguageMap()
        {
            this.ToTable("Language");
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(100);
            this.Property(p => p.LanguageCulture).IsRequired().HasMaxLength(20);
            this.Property(p => p.UniqueSeoCode).HasMaxLength(4);
        }

    }

}
