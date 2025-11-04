/*
 * Author: Hearto Lazor
 * 
 * HeartoLazor's utilitary mod for disabling the 'bobbing' effect that stardew engine introduces in movement animations. Useful for Fashion Sense's mods that uses custom body animations with baked displacements in the sprites.
 */

using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using static StardewValley.FarmerSprite;

namespace BobbingDisabler
{
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
        }

        /*********
        ** Private methods
        *********/

        /// <summary>Raised on every tick.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {

            Farmer player = Game1.player;
            if(player == null)
            {
                return;
            }
            CheckSpriteBobbing(player);
        }

        /// <summary>Disables the sprite bobbing for a player.</summary>
        /// <param name="player">The farmer to adisable the sprite bobbing.</param>
        private void CheckSpriteBobbing(Farmer player)
        {
            FarmerSprite sprite = player.FarmerSprite;
            List<AnimationFrame> anim = sprite.currentAnimation;
            int currentFrameIndex = player.FarmerSprite.currentAnimationIndex;
            if (currentFrameIndex < 0 || currentFrameIndex >= anim.Count)
            {
                return;
            }
            AnimationFrame frame = anim[currentFrameIndex];
            frame.positionOffset = 0;
            anim[currentFrameIndex] = frame;
        }
    }
}