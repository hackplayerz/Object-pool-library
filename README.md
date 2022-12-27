# Object-pool-library
오브젝트 풀을 라이브러리로 만들어 관리

> 유니티 dll(UnityEngine.dll)을 임포트하여 제작<br>
> 참조->UnityEngine.dll <br>
> 빌드된 dll은 Assets/Script/Plugin에 넣어서 사용 가능

```cs
public void Create(); // 풀안에 객체를 생성하는 함수
public T Pop(); // 풀에서 객체를 빼옴
public void Clear(); //  풀 초기화
```
