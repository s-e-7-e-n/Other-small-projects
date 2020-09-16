import pygame
from colorsys import rgb_to_hsv, hsv_to_rgb
from random import randint
pygame.init()

size_of_window   = (840, 620)    # размеры окна
x                = 400            # координаты
y                = 300            # головы
run              = True
score            = 0
num_of_food      = 50            # количество еды
speed            = 5             # смещение
way_move         = 'rigth'       # направление передвижения
delay_count_move = 0             # переменная для задержки перед смещением
arr_of_food      = []            # массив с едой пока только одна
snake_parts      = []            # массив с частями змейки
sp_without_one   = []            # массив частей без головы
# coord_parts      = [[x, y]]      # вроде бы не нужная штука
curent_part      = 0             # текущая часть змейки
color_p_hsv      = (0, 230, 230) # её цвет
energy           = 10
x_range = [ z for z in range(num_of_food)]
y_range = [ z for z in range(num_of_food)]

main_window      = pygame.display.set_mode(size_of_window) # основной дислей



class Snake:
    width = 20 # ширина обькта
    def __init__(self, x, y, color):
        self.x = x                  # его
        self.y = y                  # координаты
        self.color = color      # цвет


    # функция отрисовки обьекта
    def draw(self, win):
        pygame.draw.rect(win, self.color, (self.x, self.y, Snake.width, Snake.width))


class Food(Snake):
    def __init__(self):
        # координады еды в сетке
        self.x = randint(0, (size_of_window[0] - Food.width)//20 ) * 20
        self.y = randint(0, (size_of_window[1] - Food.width)//20 ) * 20
        self.color = (114, 0, 36)
        self.block_x = self.x // 20
        self.block_y = self.y // 20

    def draw(self, win):
        pygame.draw.rect(win, self.color, (self.x, self.y, Snake.width, Snake.width))


# # Функция изменения направления передвижения
def give_way(keys):
    global way_move
    if (keys[pygame.K_LEFT] or keys[pygame.K_a]) and (way_move != 'right'):
        way_move = 'left'

    if (keys[pygame.K_RIGHT] or keys[pygame.K_d]) and (way_move != 'left'):
        way_move = 'right'

    if (keys[pygame.K_DOWN] or keys[pygame.K_s]) and (way_move != 'up'):
        way_move = 'down'

    if (keys[pygame.K_UP] or keys[pygame.K_w]) and (way_move != 'down'):
        way_move = 'up'


# Функция двигающая обьект
def move_coors():
    global x, y
    if len(snake_parts) != 1:
        for c in range(curent_part, 0, -1):
            snake_parts[c].x = snake_parts[c-1].x
            snake_parts[c].y = snake_parts[c-1].y
    if  way_move == 'right':
        snake_parts[0].x += Snake.width
        # snake_parts[0].x %= size_of_window[0] # выход с другой стороны
    if way_move == 'left':
        snake_parts[0].x -= Snake.width
        # snake_parts[0].x %= size_of_window[0]
    if way_move == 'down':
        snake_parts[0].y += Snake.width
        # snake_parts[0].y %= size_of_window[1]
    if way_move == 'up':
        snake_parts[0].y -=  Snake.width
        # snake_parts[0].y %= size_of_window[1]

# проверка всех кусочков на сьеденость
curr_food = 0 # номер съеденого
def proof_eat():
    global curr_food
    prooff = False
    for q in range(num_of_food):
        if arr_of_food[q].x == snake_parts[0].x and arr_of_food[q].y == snake_parts[0].y:
            prooff = True
            curr_food = q
            break
    return prooff

# проделывание всего что происходит в время съедения
def event_eaten():
    global curent_part, score, curr_food, sp_without_one, energy
    proof = proof_eat()
    if proof:
        # print(sp_without_one)
        energy += 8

        # удаление съеденого и добавление нового
        arr_of_food.remove(arr_of_food[curr_food])
        food = Food()
        arr_of_food.append(food)

        # score
        score += 1
        # print(score)

        # добавление новой части змейки
        cx = 0
        cy = 0
        # возможно это лишнее или можно переделать
        # ниже зависимость места появления части от напрвления движения
        if way_move == 'left':
            cx = snake_parts[curent_part].x + 20
            cy = snake_parts[curent_part].y
        elif way_move == 'right':
            cx = snake_parts[curent_part].x - 20
            cy = snake_parts[curent_part].y
        elif way_move == 'down':
            cx = snake_parts[curent_part].x
            cy = snake_parts[curent_part].y - 20
        elif way_move == 'up':
            cx = snake_parts[curent_part].x
            cy = snake_parts[curent_part].y + 20
        # coord_parts.append([cx, cy]) # что-то лишнее(надо проверить)
        new_color = return_color(snake_parts[curent_part].color)
        snake = Snake(cx, cy, new_color) # новая часть
        snake_parts.append(snake) # добвление её в массив частей
        curent_part += 1 # новая текущая часть
        # print(snake_parts[curent_part].color)
        sp_without_one = snake_parts[1:curent_part+1]

# функция отрисовки
font_ = pygame.font.Font(None, 20)
def draw_func():
    global x_range, y_range
    main_window.fill((230, 220, 190))            # цвет фона
    for f in range(num_of_food):
        arr_of_food[f].draw(main_window)
        # x_range = list(map(lambda x: arr_of_food[f] ))
    text_score = font_.render("Счёт {0}".format(score), 1, (0,0,0))
    main_window.blit(text_score, (0,0))


    for sn in range(len(snake_parts)-1, -1, -1):
        snake_parts[sn].draw(main_window)

    pygame.display.update()

# столкновение с границами и с собой
def collision_border():
    global run, curent_part, sp_without_one, energy
    if snake_parts[0].x < 0 or snake_parts[0].x > size_of_window[0]-Snake.width or snake_parts[0].y < 0 or snake_parts[0].y > size_of_window[1]-Snake.width:
        print('You lost. you score =', score, 'Wow!')
        run = False
    # отрезание части змеи если она наступает на себя
    for i in range(len(sp_without_one)):
        if  snake_parts[0].x == sp_without_one[i].x:
            if snake_parts[0].y == sp_without_one[i].y:
                # print("SOS")
                # print(curent_part)
                sp_without_one = snake_parts[1: i]
                energy -= len(snake_parts[i + 1: len(snake_parts)])
                del snake_parts[i + 1: len(snake_parts)]
                curent_part = snake_parts.index(snake_parts[-1])
                # print(curent_part)
                break


# возвращает новый цвет по градиенту
def return_color(color):
    old_color = list(rgb_to_hsv(color[0], color[1], color[2])) # rgb to hsv
    old_color[0] = (old_color[0] + 1/360) % 360 # смещение цвета на 1 градус(hsv)
    new_color = list(hsv_to_rgb(old_color[0], old_color[1], old_color[2])) # перевод в rgb
    # new_color = tuple(map(lambda x: round(x * 256), new_color))
    return new_color


#------------------------------------MAIN---------------------------------------
color_p_rgb = (23, 161, 230)
snake = Snake(x, y, color_p_rgb)                  # создаем обьект
snake_parts.append(snake)

coord_block_of_parts = [[snake_parts[0].x // 20, snake_parts[0].y // 20]]
for ff in range(num_of_food):
    new_food = Food()
    arr_of_food.append(new_food)
wayy = 0
# Игровой цикл
while run:
    pygame.time.delay(5)               # Задерка перед начало нового цигла
                                        # сделана для того что бы можно было смещать обьект в интевале


    for event in pygame.event.get():    # события нажатия на выход
        if event.type == pygame.QUIT:
            run = False
    keys = pygame.key.get_pressed()
    if keys[pygame.K_SPACE]:
        continue


    give_way(keys)                       # проверяем на смену направления
    # give_way(wayy)

    delay_count_move += 1
    if delay_count_move == 10:
        move_coors()                     # смещаем каждые 150мс (10 * 15)
        energy -= 1
        delay_count_move = 0

    collision_border()
    event_eaten()
    draw_func()
    # NeuroSnake.get_side()
pygame.quit()
