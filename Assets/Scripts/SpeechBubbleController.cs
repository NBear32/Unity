using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBubbleController : MonoBehaviour
{

    public GameObject bubble;
    public TextMeshPro speechText;
    public string speech = "";
    float timer = 0.0f;
    bool isSpeech = false;
    bool isInCol = false;

    string[] SpeechList = {
        "안녕하세요! 오늘 강의에 오신 것을 환영해요.",
        "2층으로 올라가려면 계단에서 포털을 통과하세요.",
        "공지사항은 1층 중앙의 게시판에서 확인해 주세요.",
        "강의 리스트는 1층 오른쪽 방에서 확인할 수 있어요.",
        "강의는 기본부터 고급 기능까지 다룰 예정이예요.",
        "강의는 동영상과 강의자료로 진행되요.",
        "PDF 강의 자료를 열람할 수 있어요.",
        "강의 종료 후, 학습한 내용을 복습하는 시간을 가지세요.",
        "강의 후에 퀴즈게임이 있을 예정입니다.잘 준비해 보세요!",
        "궁금한 점이 있으면 언제든지 2층 오른쪽 질의응답 방으로 와서 문의해 주세요.",
        "기술적인 문제로 인해 도움이 필요하면, 2층 오른쪽 질의응답 방으로 와서 문의해 주세요.",
        "이벤트는 2층 중앙의 방에서 확인해 주세요.",
        "장바구니는 2층 중앙 방의 왼쪽 캐비넷에 있어요.",
        "마이페이지는 1층 왼쪽방에 있어요.",
        "마이페이지에는 회원정보, 나의학습, 회원탈퇴 메뉴가 있어요.",
        "퀴즈게임을 하려면 1층 왼쪽방으로 가세요.",
        "퀴즈게임을 하려면 [나의학습] [학습중] [강의실] 에서 퀴즈풀기를 선택해야 해요.",
        "퀴즈게임에는 부스터도 있어요.",
        "빨리 달리려면 쉬프트키를 같이 누르세요.",
        "포털에서 F키 또는 스페이스키를 누르면 통과할 수 있어요.",
        "안녕하세요! 오늘 날씨가 참 좋네요. 산책하기 딱 좋은 날입니다.",
        "DIY 프로젝트를 시도해 보신 적이 있나요? 저는 최근에 작은 가구를 만들었어요.",
        "가장 좋아하는 계절은 무엇인가요?",
        "가장 좋아하는 식사는 무엇인가요?",
        "가장 좋아하는 영화 장르는 무엇인가요?",
        "가장 좋아하는 영화 장르는 무엇인가요? 저는 드라마 장르를 좋아해요.",
        "가장 좋아하는 외식 장소가 어디인가요?",
        "가장 좋아하는 외식 장소는 근처의 스시 레스토랑이에요. 신선한 스시가 맛있죠.",
        "가장 좋아하는 정리 방법이 있나요? 저는 카테고리별로 정리하는 걸 좋아해요.",
        "간편하게 만들 수 있는 요리법이 있나요? 최근에 간단한 레시피를 발견했어요.",
        "건강을 유지하는 비결이 있나요? 저는 규칙적인 식사와 충분한 수면을 중요하게 생각해요.",
        "게임을 좋아하시나요? 최근에 재미있는 게임을 발견했어요.",
        "극장에 자주 가시나요? 최근에 본 영화가 정말 좋았어요.",
        "가까운 도서관에 다양한 책들이 있어서 자주 가요.",
        "가족과 함께하는 시간이 제일 소중해요.",
        "근처에 작은 카페가 있는데, 커피가 정말 맛있어요.",
        "근처에 좋은 체육관이 있어요. 다양한 운동 기구가 있어서 좋아요.",
        "새로운 장소를 탐험하는 걸 좋아해요.특히 해변이 좋습니다.",
        "새로운 친구를 사귀게 되었어요. 대화가 즐거워요.",
        "새로운 프랑스 레스토랑을 발견했어요. 음식이 정말 맛있어요.",
        "오늘 뉴스에서 새로운 기술 발전 소식을 봤어요.정말 흥미로웠습니다.",
        "저희 동네에 맛있는 이탈리안 레스토랑이 있어요. 자주 가죠.",
        "다음 여행지로 계획하고 있는 곳이 있나요? 저는 이곳을 가고 싶어요.",
        "다음 휴가 때 어떤 곳에 가고 싶으신가요?",
        "다음 휴가는 해변에 가고 싶어요. 바다와 모래사장을 즐기고 싶네요.",
        "돈 관리 어떻게 하세요? 저는 예산을 세우고 지키려고 노력해요.",
        "맞아요.이렇게 맑은 날엔 바깥에 나가야겠어요!",
        "매운 음식을 잘 드시나요? 저는 매운 음식을 좋아하지만 자주 먹지는 않아요.",
        "문학 작품 중에서 좋아하는 작가가 있나요? 저는 최근에 이 작가의 책을 읽었어요.",
        "문화 행사에 자주 참여하시나요? 저는 지역 축제나 행사에 가는 걸 좋아해요.",
        "미술관이나 박물관을 자주 가시나요? 저는 자주 방문해서 새로운 전시를 봐요.",
        "봄을 좋아해요. 따뜻하고 꽃들이 피는 게 아름다워요.",
        "새로운 음식을 시도해 본 적이 있나요? 저는 최근에 이국적인 요리를 먹어봤어요.",
        "스트레스를 줄이기 위한 방법이 있나요? 저는 가끔 산책을 하면서 긴장을 풀어요.",
        "시간 관리 팁이 있나요? 저는 일정표를 사용해서 하루를 계획해요.",
        "식사 후 디저트를 좋아하시나요? 저는 초콜릿 케이크를 좋아해요."
    };

    // Start is called before the first frame update
    void Start()
    {
        bubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpeech)
        {
            timer += Time.deltaTime;
            if (timer > 3.0f)
            {
                isSpeech = false;
                if (isInCol == false)
                {
                    bubble.SetActive(false);
                }
                timer = 0.0f;
                Debug.Log("TimeSet");
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (isSpeech) return;

            bubble.SetActive(true);
            if (speech == "")
            {
                int randomIndex = Random.Range(0, SpeechList.Length - 1);
                speechText.text = SpeechList[randomIndex];
            }
            else
            {
                speechText.text = speech;
            }
        }
        isSpeech = true;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isInCol = true;
            Debug.Log("ColTrue");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isInCol = false;
            Debug.Log("ColFalse");
            if (col.gameObject.tag == "Player" && isSpeech == false)
            {
                bubble.SetActive(false);
            }
        }
    }
}
