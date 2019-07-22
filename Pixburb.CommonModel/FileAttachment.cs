﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.CommonModel
{
    public class FileAttachment
    {
        public int Id { get; set; }
        public byte[] FileContent { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string RecordStatus { get; set; }
    }
}
