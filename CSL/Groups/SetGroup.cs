using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CSL.Groups
{
    /// <summary>
    /// Class that represents Set.
    /// It inherits PureGroup class.
    /// </summary>
    public class SetGroup : PureGroup, IEnumerator, IEnumerable
    {        
        internal HashSet<uint> m_set;
        private uint elementsCount;
        private int Position = -1;

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            if (Position < m_set.Count-1)
            { 
                ++Position;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            Position = -1;
        }

        public object Current
        {
            get
            {
                return m_set.ElementAt(Position);
            }
        }

        public SetGroup()
        {            
            m_set = new HashSet<uint>();
            elementsCount = 0;
        }

        public SetGroup(uint count)
        {
            m_set = new HashSet<uint>();

            elementsCount = count;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {
            for (uint i = 0; i < elementsCount; i++)
            {
                m_set.Add(i);
            }            
        }     

        /// <summary>
        /// Removes all elements from set.
        /// </summary>
        public override void Zero()
        {
            m_set.Clear();            
        }

        public override bool Find(ref uint element, FindParameters parameter)
        {
            switch(parameter)
            {
                case FindParameters.FIRST:
                    if (FindFirst(ref element))
                    {
                        return true;
                    }
                    break;
                case FindParameters.LAST:
                    if (FindLast(ref element))
                    {
                        return true;
                    }
                    break;
                case FindParameters.ANY:
                    if (FindAny(ref element))
                    {
                        return true;
                    }
                    break;
                case FindParameters.MAX:
                    if (FindMax(ref element))
                    {
                        return true;
                    }
                    break;
                case FindParameters.MIN:
                    if (FindMin(ref element))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        private bool FindMin(ref uint element)
        {
            try
            {
                long min = m_set.First();

                for (int i = 0; i < m_set.Count; i++)
                {
                    if (m_set.ElementAt(i) < min)
                    {
                        min = m_set.ElementAt(i);
                    }
                }

                element = (uint)min;
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        private bool FindMax(ref uint element)
        {
            try
            {
                long max = m_set.First();

                for (int i = 0; i < m_set.Count; i++)
                {                    
                    if (m_set.ElementAt(i) > max)
                    {
                        max = m_set.ElementAt(i);
                    }
                }

                element = (uint)max;
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        private bool FindMax(ref uint element, PureGroup.PBoolDelegate function)
        {
            try
            {
                long max = -1;
                bool found = false;

                for (int i = 0; i < m_set.Count; i++)
                {
                    if( (m_set.ElementAt(i) > max) && (function(m_set.ElementAt(i))))
                    {
                        found = true;
                        max = m_set.ElementAt(i);
                    }
                }
                if (found)
                {
                    element = (uint)max;
                    return true;
                }

                return false;
                
            }
            catch (Exception)
            {
                return false;
            }            
        }

        private bool FindMin(ref uint element, PureGroup.PBoolDelegate function)
        {
            try
            {
                long min = uint.MaxValue;
                bool found = false;

                for (int i = 0; i < m_set.Count; i++)
                {
                    if ((m_set.ElementAt(i) < min) && (function(m_set.ElementAt(i))))
                    {
                        found = true;
                        min = m_set.ElementAt(i);
                    }
                }

                if (found)
                {
                    element = (uint)min;
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        private bool FindFirst(ref uint element)
        {
            bool found = false;
            long setElement = -1;

            try
            {
                setElement = m_set.First();
                element = (uint)setElement;
                found = true;
            }
            catch (InvalidOperationException)
            {
                found = false;
            }

            return found;        
        }

        private bool FindFirst(ref uint element, PureGroup.PBoolDelegate function)
        {
            try
            {
                foreach (long current in m_set)
                {
                    if (function((uint)current))
                    {
                        element = (uint)current;
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        private bool FindLast(ref uint element, PureGroup.PBoolDelegate function)
        {
            try
            {
                List<uint> temp = m_set.ToList();
                temp.Reverse();

                foreach (long current in temp)
                {
                    if (function((uint)current))
                    {
                        element = (uint)current;
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        private bool FindLast(ref uint element)
        {
            bool found = false;
            long setElement = -1;

            try
            {
                setElement = m_set.Last();
                element = (uint)setElement;
                found = true;
            }
            catch (InvalidOperationException)
            {
                found = false;
            }

            return found;
        }

        bool FindAny(ref uint element)
        {
            bool found = false;
            long setElement = -1;

            try
            {
                int count = m_set.Count;
                int index = count / 2;
                setElement = m_set.ElementAt(index);
                element = (uint)setElement;
                found = true;
            }
            catch (InvalidOperationException)
            {
                found = false;
            }

            return found;
        }

        public override bool Find(ref uint element, FindParameters parameter, PureGroup.PBoolDelegate function)
        {
            switch(parameter)
            {
                case FindParameters.FIRST:
                case FindParameters.ANY:
                    if (FindFirst(ref element, function))
                    {
                        return true;
                    }
                    break;
                case FindParameters.LAST:
                    if (FindLast(ref element, function))
                    {
                        return true;
                    }
                    break;
                case FindParameters.MAX:
                    if (FindMax(ref element, function))
                    {
                        return true;
                    }
                    break;
                case FindParameters.MIN:
                    if (FindMin(ref element, function))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        public override int Loses(PureGroup group)
        {
            SetGroup tempSet = new SetGroup();
            tempSet = group as SetGroup;
            try
            {
                foreach (uint element in tempSet)
                {
                    m_set.Remove(element);
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }            
        }

        public override int Gains(PureGroup group)
        {               
            try
            {
                SetGroup tempSet = group as SetGroup;
                this.m_set.UnionWith(tempSet.m_set);
                this.elementsCount = (uint)this.m_set.Count;
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override int Converse(PureGroup group)
        {
            try
            {
                SetGroup temp = group as SetGroup;
                m_set.Clear();
                for (uint i = 0; i < elementsCount; i++)
                {
                    if (!temp.m_set.Contains(i))
                    {
                        this.m_set.Add(i);
                    }
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
              
        }

        public override int In(uint j)
        {
            foreach (uint element in m_set)
            {
                if (element == j)
                {                    
                    return 1;
                }
            }
            return 0;            
        }

        public override int NotIn(uint j)
        {
            foreach (uint element in m_set)
            {
                if (element == j)
                {
                    return 0;
                }
            }
            return 1;  
        }

        public override int Equals(PureGroup group)
        {
            SetGroup tempSet = new SetGroup();
            tempSet = group as SetGroup;            

            if (tempSet.m_set.Count == this.m_set.Count)
            {
                foreach (uint element in tempSet)
                {
                    if (!(m_set.Contains(element)))
                    {                        
                        return 0;
                    }
                }
                return 1;
            }
            else
            {
                return 0;
            }            
        }

        /// <summary>
        /// Checks if current set is subset of parameter set.
        /// </summary>
        /// <param name="group">Set to check, if it is superset.</param>
        /// <returns>1 if current set is within parameter set, 0 otherwise.</returns>
        public override int Within(PureGroup group)
        {
            try
            {
                SetGroup tempSet = group as SetGroup;

                if (this.m_set.IsSubsetOf(tempSet.m_set))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }                   
        }

        public override int Empty()
        {
            if (this.m_set.Count == 0)
            {
                return 1;            
            }                
            else
            {
                return 0;
            }            
        }

        public override int Head(uint j)
        {
            To(j);
            return 1;
        }

        public override int Tail(uint j)
        {
            To(j);
            return 1;
        }


        /// <summary>
        /// Perform method delegate action for every element in set.
        /// </summary>
        /// <param name="pureGroupDelegate">Delegate method.</param>
        /// <returns>1 if success, -1 otherwise.</returns>
        public override int For(PureGroup.PBoolDelegate pureGroupDelegate)
        {
            try
            {
                foreach (uint element in this.m_set)
                {
                    pureGroupDelegate(element);
                }

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }            
        }


        /// <summary>
        /// Summs results of method for every element in set.
        /// </summary>
        /// <param name="groupDelegate">Delegate method.</param>
        /// <returns>Sum of all elements or -1 if something goes wrong.</returns>
        public override long Sum(PureGroup.PValDelegate groupDelegate)
        {
            long sum = 0;
            try
            {
                foreach (uint element in m_set)
                {
                    sum += groupDelegate(element);
                }

                return sum;
            }
            catch (Exception)
            {
                return -1;
            }            
        }

        /// <summary>
        /// Checks, if certain elements number match delegate method condition.
        /// </summary>
        /// <param name="j">Number of elements to match the condition.</param>
        /// <param name="groupDelegate">Delegate method that checks condition.</param>
        /// <returns>1 if number of elements that match the condition is j, otherwise -1.</returns>
        public override int Unique(uint j, PureGroup.PBoolDelegate groupDelegate)
        {
            uint uniqueCount = 0;
            try
            {
                foreach (uint element in m_set)
                {
                    if (groupDelegate(element))
                    {
                        uniqueCount++;
                    }
                }

                if (uniqueCount == j)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
                
            }
            catch (Exception)
            {
                return -1;
            }      
        }

        public override int Count(PureGroup.PBoolDelegate groupDelegate)
        {
            int count = 0;
            try
            {
                foreach (uint element in m_set)
                {
                    if(groupDelegate(element))
                    {
                        count++;
                    }
                }

                return count;
            }
            catch (Exception)
            {
                return -1;
            }            
        }

        public override int Exists(uint j, PureGroup.PBoolDelegate groupDelegate)
        {
            uint count = 0;
            try
            {
                foreach (uint element in m_set)
                {
                    if (groupDelegate(element))
                    {
                        count++;
                    }
                }

                if (count >= j)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception)
            {
                return -1;
            }   
        }

        public override int All(PureGroup.PBoolDelegate pureGroupDelegate)
        {            
            try
            {
                foreach (uint element in m_set)
                {
                    if (!pureGroupDelegate(element))
                    {
                        return -1;
                    }
                }

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }    
        }

        public override int Rank(PureGroup.PValDelegate groupDelegate)
        {
            /// Zbiór jest nieuporządkowany więc nie sortujemy.

            return 0;
        }

        public override void To(uint elementToAdd)
        {
            m_set.Add(elementToAdd);            
            currentCount = m_set.Count;
        }

        public override void From(uint element)
        {
            m_set.Remove(element);
            currentCount = m_set.Count;
        }

        public override int Split(PureGroup.PBoolDelegate f1, ref PureGroup group1, ref PureGroup group2, PureGroup.PBoolDelegate f2)
        {
            try
            {
                foreach (uint element in m_set)
                {
                    if (f2(element))
                    {
                        if (f1(element))
                        {
                            group1.To(element);
                        }
                        else
                        {
                            group2.To(element);
                        }
                    }
                }

                return 1;

            }
            catch (Exception)
            {
                return 0;
            }            
        }

    }
}
