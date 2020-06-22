using System;

namespace Code.DataEntities
{
   [Serializable]
   public struct ApartmentsProjectData
   {
      public int id;
      public string title;
      public float[] coordinates;
      public string grade;
      public float totalCost;
      public int flatsCount;
   }
}