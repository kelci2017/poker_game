using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameKelci
{
    public class Card
    {
        /**
	 * type of the Card(heart,spade,club,diamond,red king,black king)
	 */
        private int card_type;
        
        private int number;

        /**
         * 
         * @param card_type
         * @param number
         */
        public Card(int card_type, int number)
        {
            this.card_type = card_type;
            this.number = number;
        }


        /**
         * getter for card_type
         * @return
         */
        public int getCard_type()
        {
            return this.card_type;
        }
        /**
         * getter for number
         * @return
         */
        public int getNumber()
        {
            return this.number;
        }

        /**
         * This method is only for print purpose.
         * @return
         */
        public String getCard_type_desc()
        {
            switch (card_type)
            {
                case 1:
                    return Constants.STR_CARD_TYPE_HEART;
                case 2:
                    return Constants.STR_CARD_TYPE_SPADE;
                case 3:
                    return Constants.STR_CARD_TYPE_CLUB;
                case 4:
                    return Constants.STR_CARD_TYPE_DIAMOND;
                case 5:
                    return Constants.STR_CARD_TYPE_RED_KING;
                case 6:
                    return Constants.STR_CARD_TYPE_BLACK_KING;
                default:
                    return card_type + "";
            }
        }
    }
}
