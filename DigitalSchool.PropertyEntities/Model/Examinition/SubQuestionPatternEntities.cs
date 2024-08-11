using DS.PropertyEntities.Model.ManagedBatch;
using DS.PropertyEntities.Model.ManagedClass;
using DS.PropertyEntities.Model.ManagedSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Examinition
{
    public class SubQuestionPatternEntities:IDisposable
    {
        public int SubQPId { get; set; }      
        public ExamTypeEntities ExamType { get; set; }
        public BatchEntities Batch { get; set; }
        //public GroupEntities Group { get; set; }
        public ClassGroupEntities Group { get; set; }
        public SubjectEntities Subject { get; set; }
        public CourseEntity Course { get; set; }
        public QuestionPatternEntities QPattern { get; set; }
        public float QMarks { get; set; }
        public float PassMarks { get; set; }
        public float SubQPMarks { get; set; }
        public float ConvertTo { get; set; }
        public bool IsOptional { get; set; }
        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            disposed = true;
        }
    }
}
