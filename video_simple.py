import cv2
import mediapipe as mp
import time

# 单帧处理
def process_frame(img):
    img=cv2.flip(img,1)
    img_RGB=cv2.cvtColor(img,cv2.COLOR_BGR2RGB)
    results=hands.process(img_RGB)

    # 如果有检测到手
    if results.multi_hand_landmarks:  # 如果有检测到手
        # 遍历每一只检测出的手
        for hand_idx in range(len(results.multi_hand_landmarks)):
            hand_21 = results.multi_hand_landmarks[hand_idx]  # 获取该手的所有关键点坐标
            mpDraw.draw_landmarks(img, hand_21, mp_hands.HAND_CONNECTIONS)  # 可视化
        # 在三维坐标系中可视化索引为0的手
        # mpDraw.plot_landmarks(results.multi_hand_landmarks[0], mp_hands.HAND_CONNECTIONS)
    return img

# 调用摄像头获取帧数
def use_camera():
    cap=cv2.VideoCapture(0)
    cap.open(1)

    # time.sleep(2)

    while cap.isOpened():
        success,frame=cap.read()
        if not success:
            print('Error')
            break

        # 处理帧
        frame=process_frame(frame)

        # print(frame.size)
        if frame is not None:
            cv2.imshow('hands',frame)

        if cv2.waitKey(1) in [ord('q'),27]:
            break

    cap.release()
    cv2.destroyAllWindows()

if __name__ == '__main__':
    mp_hands = mp.solutions.hands

    hands = mp_hands.Hands(static_image_mode=False,  # 是静态图片还是连续视频帧
                           max_num_hands=2,  # 最多检测几只手
                           min_detection_confidence=0.7,  # 置信度阈值
                           min_tracking_confidence=0.5)  # 追踪阈值

    # 导入绘图函数
    mpDraw = mp.solutions.drawing_utils

    use_camera()
