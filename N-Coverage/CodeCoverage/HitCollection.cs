using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace NCoverage.CodeCoverage
{
	/// <summary>
	/// manages a collection of addresses (RVA) and the corresponding hit counts
	/// hits can be merged/diffed with other classes
	/// addresses are converted to RVAs when added
	/// </summary>
	public class HitSet : IEnumerable<Hit>
	{
		private Dictionary<uint, int> hits_;
		private uint imageBase_;

		public HitSet()
		{
			hits_ = new Dictionary<uint, int>();
		}

		public HitSet(uint imageBase) 
			: this()
		{
			imageBase_ = imageBase;
		}

		public IEnumerator<Hit> GetEnumerator()
		{
			return new HitSetEnum(hits_);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HitSetEnum(hits_);
		}

		/// <summary>
		/// add address (VA) to internal hash table and silently convert to RVA
		/// </summary>
		/// <param name="address"></param>
		public void addHit(uint address)
		{
			uint rva = address - imageBase_;
			if (hits_.ContainsKey(rva)) hits_[rva]++;
			else hits_.Add(rva, 1);
		}

		/// <summary>
		/// add all hits from srcHits to our instance
		/// </summary>
		/// <param name="srcHits"></param>
		public void merge(HitSet srcHits)
		{
			foreach (Hit h in srcHits)
			{
				if (hits_.ContainsKey(h.Address)) hits_[h.Address] += h.Count;
				else hits_[h.Address] = h.Count;
			}
		}

		public int Count
		{
			get { return hits_.Count; }
		}

		/// <summary>
		/// remove all hits in srcHits from our instance
		/// </summary>
		/// <param name="srcHits"></param>
		public void filter(HitSet srcHits)
		{
			foreach (Hit h in srcHits)
			{
				if (hits_.ContainsKey(h.Address)) hits_.Remove(h.Address);
			}
		}

		/// <summary>
		/// we might need the IBA to convert hit addresses to RVAs
		/// </summary>
		public uint ImageBase
		{
			get { return imageBase_; }
			set { imageBase_ = value; }
		}
	}

	public struct Hit
	{
		public Hit(uint address, int count)
		{
			Address = address;
			Count = count;
		}
		public uint Address;
		public int Count;
	}

	/// <summary>
	/// class to enumerate all key value pairs in the hits dictionary and return them as Hit structs
	/// </summary>
	public class HitSetEnum : IEnumerator<Hit>
	{
		private Dictionary<uint, int> hits_;
		private Dictionary<uint, int>.Enumerator hitEnumerator_;
		private int position_;

		public HitSetEnum(Dictionary<uint, int> hits)
		{
			hits_ = hits;
			position_ = -1;
			hitEnumerator_ = hits.GetEnumerator();
		}

		public Hit Current
		{
			get
			{
				try
				{
					return new Hit(hitEnumerator_.Current.Key, hitEnumerator_.Current.Value);
				}
				catch (IndexOutOfRangeException)
				{
					throw new InvalidOperationException();
				}
			}
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		object IEnumerator.Current
		{
			get
			{
				try
				{
					return new Hit(hitEnumerator_.Current.Key, hitEnumerator_.Current.Value);
				}
				catch (IndexOutOfRangeException)
				{
					throw new InvalidOperationException();
				}
			}
		}

		public bool MoveNext()
		{
			++position_;
			hitEnumerator_.MoveNext();
			return (position_ < hits_.Count);
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}
}
