﻿using System;

namespace WebExpress.WebApp.WebIndex.Storage
{
    public interface IIndexStorageDataStructureListItem : IIndexStorageDataStructure, IComparable
    {
        /// <summary>
        /// Returns or sets the address of the following list item.
        /// </summary>
        public ulong SuccessorAddr { get; set; }
    }
}
