using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Rainbow.DapperExtensions
{
    public interface IMultipleResultReader
    {
        IEnumerable<T> Read<T>();
        T ReadFirstOrDefault<T>();
        Task<IEnumerable<T>> ReadAsync<T>();
        Task<T> ReadFirstOrDefaultAsync<T>();

    }

    public class GridReaderResultReader : IMultipleResultReader
    {
        private readonly SqlMapper.GridReader _reader;

        public GridReaderResultReader(SqlMapper.GridReader reader)
        {
            _reader = reader;
        }

        public IEnumerable<T> Read<T>()
        {
            if (_reader.IsConsumed) return default;
            return _reader.Read<T>();
        }

        public T ReadFirstOrDefault<T>()
        {
            if (_reader.IsConsumed) return default;
            return _reader.ReadFirstOrDefault<T>();
        }

        public async Task<IEnumerable<T>> ReadAsync<T>()
        {
            if (_reader.IsConsumed) return default;
            return await _reader.ReadAsync<T>();
        }

        public async Task<T> ReadFirstOrDefaultAsync<T>()
        {
            if (_reader.IsConsumed) return default;
            return await _reader.ReadFirstOrDefaultAsync<T>();
        }
    }

    public class SequenceReaderResultReader : IMultipleResultReader
    {
        private readonly Queue<SqlMapper.GridReader> _items;

        public SequenceReaderResultReader(IEnumerable<SqlMapper.GridReader> items)
        {
            _items = new Queue<SqlMapper.GridReader>(items);
        }

        public IEnumerable<T> Read<T>()
        {
            SqlMapper.GridReader reader = _items.Dequeue();
            if (reader.IsConsumed) return default;
            return reader.Read<T>();
        }

        public T ReadFirstOrDefault<T>()
        {
            SqlMapper.GridReader reader = _items.Dequeue();
            if (reader.IsConsumed) return default;
            return reader.ReadFirstOrDefault<T>();
        }

        public async Task<IEnumerable<T>> ReadAsync<T>()
        {
            SqlMapper.GridReader reader = _items.Dequeue();
            if (reader.IsConsumed) return default;
            return await reader.ReadAsync<T>();
        }

        public async Task<T> ReadFirstOrDefaultAsync<T>()
        {
            SqlMapper.GridReader reader = _items.Dequeue();
            if (reader.IsConsumed) return default;
            return await reader.ReadFirstOrDefaultAsync<T>();
        }
    }
}