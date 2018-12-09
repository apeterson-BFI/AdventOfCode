using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCSharp
{
    public class CircularLinkedList
    {
        public CircularLinkListNode ActiveNode { get; set; }

        public int NodeCount { get; set; }

        public CircularLinkedList()
        {
            ActiveNode = new CircularLinkListNode() { Marble = 0L };
            ActiveNode.Clockwise = ActiveNode;
            ActiveNode.Counterclockwise = ActiveNode;

            NodeCount = 1;
        }

        public CircularLinkedList(long marble)
        {
            ActiveNode = new CircularLinkListNode() { Marble = marble };
            ActiveNode.Clockwise = ActiveNode;
            ActiveNode.Counterclockwise = ActiveNode;

            NodeCount = 1;
        }

        public void gameMarble(long marble)
        {
            var c1Node = ActiveNode.Clockwise;
            var c2Node = c1Node.Clockwise;

            var nNode = new CircularLinkListNode() { Marble = marble, Clockwise = c2Node, Counterclockwise = c1Node };
            c1Node.Clockwise = nNode;
            c2Node.Counterclockwise = nNode;
            ActiveNode = nNode;
        }

        public long game23k(long marble)
        {
            long score = marble;

            var remNode = ActiveNode.Counterclockwise.Counterclockwise.Counterclockwise.Counterclockwise.Counterclockwise.Counterclockwise.Counterclockwise;

            score += remNode.Marble;

            var rcc = remNode.Counterclockwise;
            var rc = remNode.Clockwise;

            rcc.Clockwise = rc;
            rc.Counterclockwise = rcc;
            ActiveNode = rc;

            return score;
        }

        public static long playGame(int players, long lastMarble)
        {
            long[] playerScores = new long[players];

            int activePlayer = 0;

            CircularLinkedList list = new CircularLinkedList();

            for (long marble = 1L; marble <= lastMarble; marble++)
            {
                if (marble % 23L == 0L)
                {
                    playerScores[activePlayer] += list.game23k(marble);
                }
                else
                {
                    list.gameMarble(marble);
                }

                activePlayer = ((activePlayer + 1) % players);
            }

            return playerScores.Max();
        }
    }
}
