using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_runner
{
    public record Day08(Program Program)
    {
        public Day08(string[] input) : this(Program.Parse(input)) { }

        public int Part1()
        {
            var executedStatementsHistory = new HashSet<int>();

            return new GameConsoleState(Program)
                  .NextSteps()
                  .TakeWhile(x => executedStatementsHistory.Add(x.StatementPointer))
                  .Last().Accumulator;
        }
    }

    public record GameConsoleState(Program Program, int StatementPointer = 0, int Accumulator = 0)
    {
        public Statement CurrentStatement => Program.Statements[StatementPointer];

        public GameConsoleState Step() => CurrentStatement.Op switch
        {
            Op.Acc => this with {
                StatementPointer = StatementPointer + 1,
                Accumulator = Accumulator + CurrentStatement.Param
                },

            Op.Nop => this with {
                StatementPointer = StatementPointer + 1,
                },

            Op.Jmp => this with {
                StatementPointer = StatementPointer + CurrentStatement.Param
                },

            _ => throw new InvalidOperationException()
        };

        public IEnumerable<GameConsoleState> NextSteps()
        {
            for (var state = Step();; state = state.Step())
                yield return state;
        }
    }

    public record Program(Statement[] Statements)
    {
        public static Program Parse(string[] input) => new(input.Select(Statement.Parse).ToArray());
    }

    public record Statement(Op Op, int Param)
    {
        public static Statement Parse(string input)
        {
            var parts = input.Split(' ');
            return new Statement(Enum.Parse<Op>(parts[0], ignoreCase: true), int.Parse(parts[1]));
        }
    }

    public enum Op
    {
        /// <summary>
        /// acc increases or decreases a single global value called the accumulator by the value given in the argument. For example,
        /// acc +7 would increase the accumulator by 7. The accumulator starts at 0. After an acc instruction, the instruction
        /// immediately below it is executed next.
        /// </summary>
        Acc,
        
        /// <summary>
        /// nop stands for No OPeration - it does nothing. The instruction immediately below it is executed next.
        /// </summary>
        Nop,
        
        /// <summary>
        /// jmp jumps to a new instruction relative to itself. The next instruction to execute is found using the argument as an
        /// offset from the jmp instruction; for example, jmp +2 would skip the next instruction, jmp +1 would continue to the instruction
        /// immediately below it, and jmp -20 would cause the instruction 20 lines above to be executed next.
        /// </summary>
        Jmp
    };

}