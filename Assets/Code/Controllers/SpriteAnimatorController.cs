using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using UnityEngine;
using Updates;

namespace Code.Controllers
{
    public class SpriteAnimatorController : IUpdatable, ICleanup
    {
        private const float DefaultAnimationSpeed = 6.0f;
        
        private readonly SpriteAnimatorConfig _config;
        private readonly List<Animation> _activeAnimations = new List<Animation>();

        public SpriteAnimatorController(SpriteAnimatorConfig config)
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer renderer, AnimationType track, bool isLooped, float speed = DefaultAnimationSpeed)
        {
            var animation = _activeAnimations.FirstOrDefault(x => x.Renderer == renderer);
            if (animation is null)
            {
                _activeAnimations.Add(new Animation(
                    renderer,
                    track,
                    _config.spriteSequences.Find(x => x.type == track).sprites,
                    isLooped,
                    speed
                    ));
            }
            else
            {
                if (animation.Track == track) return;
                animation.ResetCounter();
                animation.Track = track;
                animation.Sprites = _config.spriteSequences.Find(x => x.type == track).sprites;
                animation.IsLooped = isLooped;
                animation.AnimationSpeed = speed;
            }
        }

        public void StopAnimation(SpriteRenderer renderer)
        {
            var animation = _activeAnimations.FirstOrDefault(x => x.Renderer == renderer);
            if (animation is null) return;
            _activeAnimations.Remove(animation);
        }
        
        public void Update(float deltaTime)
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Update(deltaTime);
            }
        }

        public void Cleanup()
        {
            _activeAnimations.Clear();
        }

        private sealed class Animation
        {
            internal readonly SpriteRenderer Renderer;
            internal AnimationType Track;
            internal List<Sprite> Sprites;
            internal bool IsLooped;
            private bool _isSleeping;
            internal float AnimationSpeed;
            private float _counter = 0.0f;

            internal void Update(float deltaTime)
            {
                if (_isSleeping) return;

                _counter += deltaTime * AnimationSpeed;

                if (_counter >= Sprites.Count)
                {

                    if (IsLooped)
                    {
                        _counter -= Sprites.Count;
                        UpdateCurrentSprite();
                    }
                    else
                    {
                        _counter = Sprites.Count;
                        _isSleeping = true;
                    }
                }
                else
                {
                    UpdateCurrentSprite();
                }
                
                void UpdateCurrentSprite()
                {
                    Renderer.sprite = Sprites[(int) _counter];
                }
            }

            internal void ResetCounter()
            {
                _counter = 0.0f;
            }
            
            internal Animation(SpriteRenderer renderer, AnimationType track, List<Sprite> sprites, bool isLooped, float speed = DefaultAnimationSpeed)
            {
                Renderer = renderer;
                Track = track;
                Sprites = sprites;
                AnimationSpeed = speed;
                IsLooped = isLooped;
                _isSleeping = false;
            }
        }
    }
}