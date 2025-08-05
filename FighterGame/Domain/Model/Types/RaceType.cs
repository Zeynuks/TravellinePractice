using System.ComponentModel;

namespace FighterGame.Domain.Model.Types
{
    public enum RaceType
    {
        [Description( "Ассимар" )] Aasimar,

        [Description( "Драконорожденный" )] Dragonborn,

        [Description( "Человек" )] Human,

        [Description( "Тифлинг" )] Tiefling
    }
}