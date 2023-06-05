using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _joystick;
        [SerializeField] private float _offset;
        
        private Vector2 _pointPosition;
        
        public float Horizontal { private set; get; }
        public float Vertical { private set; get; }

        public void OnDrag(PointerEventData eventData)
        {
            var backgroundRectSize = _background.rect.size;
            var joystickRectSize = _joystick.rect.size;
            var backgroundPosition = _background.position;

            var pointPositionX = (eventData.position.x - backgroundPosition.x) / ((backgroundRectSize.x - joystickRectSize.x) / 2);
            var pointPositionY = (eventData.position.y - backgroundPosition.y) / ((backgroundRectSize.y - joystickRectSize.y) / 2);
            
            _pointPosition = new Vector2(pointPositionX, pointPositionY);
            _pointPosition = _pointPosition.magnitude > 1.0f ? _pointPosition.normalized : _pointPosition;

            var joystickPositionX = _pointPosition.x * ((backgroundRectSize.x - joystickRectSize.x) / 2) * _offset + backgroundPosition.x;
            var joystickPositionY = _pointPosition.y * ((backgroundRectSize.y - joystickRectSize.y) / 2) * _offset + backgroundPosition.y;
            
            _joystick.transform.position = new Vector2(joystickPositionX, joystickPositionY);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _pointPosition = new Vector2(0f,0f);
            _joystick.transform.position = _background.position;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData) 
        {
            OnEndDrag(eventData);
        }
        
        private void Update () 
        {
            Horizontal = _pointPosition.x;
            Vertical = _pointPosition.y;
        }
    }
}