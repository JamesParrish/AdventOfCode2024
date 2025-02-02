using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day6(IFileHelper fileHelper) : BaseDay(fileHelper), IDay
    {
        public DayEnum Day => DayEnum.Day6;

        public void Process1Star()
        {
            Console.WriteLine("Processing Day 6 - 1 Star");

            var lines = GetLines(Day, StageEnum.Stage1);

            var mapData = MapMapData(lines);

            TraverseMap(mapData);

            var visitedSpaceCount = GetVisitedSpaceCount(mapData);

            Console.WriteLine($"Total: {visitedSpaceCount}");
        }

        public void Process2Star()
        {
            Console.WriteLine("Processing Day 6 - 2 Star");

            var total = 0;

            Console.WriteLine($"Total: {total}");
        }

        private MapData MapMapData(IEnumerable<string> lines)
        {
            var mapData = new List<IList<char>>();

            foreach (var line in lines)
            {
                var mapLine = line.ToCharArray();
                mapData.Add(mapLine);
            }

            return new MapData
            {
                Data = mapData
            };
        }

        private void TraverseMap(MapData mapData)
        {
            var guard = GetInitialGuardStatus(mapData);

            while (true)
            {
                MarkGuardLocationAsVisited(mapData, guard);
                var nextCell = GetNextCellType(mapData, guard);

                if (nextCell == CellType.Outside)
                {
                    return;
                }

                MoveGuard(mapData, guard, nextCell);
            }
        }

        private Guard GetInitialGuardStatus(MapData mapData)
        {
            for (var i = 0; i < mapData.Data.Count; i++)
            {
                var lineData = mapData.Data[i];

                for (var j = 0; j < lineData.Count; j++)
                {
                    var character = lineData[j];

                    if (character.Equals('.') || character.Equals('#'))
                    {
                        continue;
                    }

                    var direction = character switch
                    {
                        '^' => Direction.Up,
                        'v' => Direction.Down,
                        '<' => Direction.Left,
                        '>' => Direction.Right,
                        _ => Direction.Unknown
                    };

                    return new Guard
                    {
                        RowLocation = i,
                        ColumnLocation = j,
                        Direction = direction
                    };
                }
            }

            return null;
        }

        private void MarkGuardLocationAsVisited(MapData mapData, Guard guard)
        {
            mapData.Data[guard.RowLocation][guard.ColumnLocation] = 'X';
        }

        private void MoveGuard(MapData mapData, Guard guard, CellType nextCell)
        {

            if (nextCell == CellType.Obstacle)
            {
                TurnGuard(guard);
            }

            switch (guard.Direction)
            {
                case Direction.Up:
                    guard.RowLocation--;
                    break;
                case Direction.Down:
                    guard.RowLocation++;
                    break;
                case Direction.Left:
                    guard.ColumnLocation--;
                    break;
                case Direction.Right:
                    guard.ColumnLocation++;
                    break;
            }
        }

        private CellType GetNextCellType(MapData mapData, Guard guard)
        {
            var rowIndex = guard.RowLocation;
            var columnIndex = guard.ColumnLocation;

            switch (guard.Direction)
            {
                case Direction.Up:
                    rowIndex--;
                    break;
                case Direction.Down:
                    rowIndex++;
                    break;
                case Direction.Left:
                    columnIndex--;
                    break;
                case Direction.Right:
                    columnIndex++;
                    break;
            }

            if (rowIndex < 0 ||
                rowIndex > mapData.Data.Count ||
                columnIndex < 0 ||
                columnIndex > mapData.Data.First().Count)
            {
                return CellType.Outside;
            }

            var cellCharacter = mapData.Data[rowIndex][columnIndex];

            return cellCharacter switch
            {
                '.' => CellType.Empty,
                'X' => CellType.Empty,
                '#' => CellType.Obstacle,
                _ => CellType.Unknown
            };
        }

        private void TurnGuard(Guard guard)
        {
            switch (guard.Direction)
            {
                case Direction.Up:
                    guard.Direction = Direction.Right;
                    break;
                case Direction.Down:
                    guard.Direction = Direction.Left;
                    break;
                case Direction.Left:
                    guard.Direction = Direction.Up;
                    break;
                case Direction.Right:
                    guard.Direction = Direction.Down;
                    break;
            }
        }

        private int GetVisitedSpaceCount(MapData mapData)
        {
            var visitedSpaceCount = 0;

            foreach (var row in mapData.Data)
            {
                visitedSpaceCount += row.Where(c => c.Equals('X')).Count();
            }

            return visitedSpaceCount;
        }

        private class MapData
        {
            public IList<IList<char>> Data { get; set; }
        }

        private class Guard
        {
            public int RowLocation { get; set; }
            public int ColumnLocation { get; set; }
            public Direction Direction { get; set; }
        }

        private enum Direction
        {
            Unknown,
            Up,
            Down,
            Left,
            Right
        }

        private enum CellType
        {
            Unknown,
            Empty,
            Obstacle,
            Outside
        }

    }
}
